using System.IO;

namespace Naak.HtmlRules
{
	internal class CaptureStream : Stream
	{
		private readonly Stream _internalStream;
		private Stream _copyStream;

		public CaptureStream(Stream internalStream)
		{
			_copyStream = new MemoryStream();
			_internalStream = internalStream;
		}

		public Stream CopyStream
		{
			get { return _copyStream; }
			set { _copyStream = value; }
		}


		public override bool CanRead
		{
			get { return _internalStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return _internalStream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return _internalStream.CanWrite; }
		}

		public override void Flush()
		{
			_internalStream.Flush();
		}

		public override long Length
		{
			get { return _internalStream.Length; }
		}

		public override long Position
		{
			get { return _internalStream.Position; }
			set { _internalStream.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return _internalStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return _internalStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			_internalStream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			_copyStream.Write(buffer, offset, count);
			_internalStream.Write(buffer, offset, count);
		}

        public string ReadToEnd()
        {
            CopyStream.Position = 0;
            using (var sr = new StreamReader(CopyStream))
            {
                return sr.ReadToEnd();
            }
        }
	}
}