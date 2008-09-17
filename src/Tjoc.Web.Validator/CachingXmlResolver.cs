using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;

namespace Tjoc.Web.Validator
{
	/// <summary>
	/// Caches responses from external resources used in the validation
	/// process. This hugely improves performance by removing the need
	/// to hit the w3c DTDs all the time.
	/// </summary>
	public class CachingXmlResolver : XmlUrlResolver
	{
		//Create a static HashTable to store the Uri - Cache file relationship.
		private static readonly Hashtable _cache = new Hashtable();
		private static readonly object _cacheLock = new object();

		// Use 32K as initial length of content buffer.
		private const int _initialBufferLength = 32768;

		/// <summary>
		/// Override of the XmlUrlResolver GetEntity Function
		/// </summary>
		/// <param name="absoluteUri">The Uri of the requested entity</param>
		/// <param name="role">n/a in this release</param>
		/// <param name="ofObjectToReturn">n/a in this release</param>
		/// <returns>System.IO.Stream of requested resource</returns>
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			var content = (byte[]) _cache[absoluteUri];

			if (content == null)
			{
				lock (_cacheLock)
				{
					// double check
					content = (byte[]) _cache[absoluteUri];

					if (content == null)
					{
						Stream stream;
						ProxySettings proxy = ValidatorModule.Config.Proxy;
						if (proxy != null && !string.IsNullOrEmpty(proxy.Server))
						{
							var request = (HttpWebRequest) WebRequest.Create(absoluteUri);
							request.Proxy = new WebProxy(proxy.Server, proxy.Port);
							request.Proxy.Credentials = new NetworkCredential(proxy.Username, proxy.Password, proxy.Domain);

							var response = (HttpWebResponse) request.GetResponse();
							stream = response.GetResponseStream();
						}
						else
						{
							stream = (Stream) base.GetEntity(absoluteUri, role, ofObjectToReturn);
						}

						// Read stream into byte array.
						content = ReadFullStream(stream);
						// cache the content of stream.
						_cache.Add(absoluteUri, content);
					}
				}
			}
			return new MemoryStream(content);
		}

		/// <summary>
		/// Reads data from a stream until the end is reached. The
		/// data is returned as a byte array. An IOException is
		/// thrown if any of the underlying IO calls fail.
		/// </summary>
		/// <param name="stream">The stream to read data from</param>
		private static byte[] ReadFullStream(Stream stream)
		{
			var buffer = new byte[_initialBufferLength];
			int read = 0;

			int chunk;
			while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
			{
				read += chunk;

				// If we've reached the end of our buffer, check to see if there's
				// any more information
				if (read == buffer.Length)
				{
					int nextByte = stream.ReadByte();

					// End of stream? If so, we're done
					if (nextByte == -1)
					{
						return buffer;
					}

					// Nope. Resize the buffer, put in the byte we've just
					// read, and continue
					var newBuffer = new byte[buffer.Length*2];
					Array.Copy(buffer, newBuffer, buffer.Length);
					newBuffer[read] = (byte) nextByte;
					buffer = newBuffer;
					read++;
				}
			}
			// Buffer is now too big. Shrink it.
			var ret = new byte[read];
			Array.Copy(buffer, ret, read);
			return ret;
		}
	}
}