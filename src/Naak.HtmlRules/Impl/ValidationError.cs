using System;

namespace Naak.HtmlRules.Impl
{
    [Serializable]
	public class ValidationError
	{
	    public ValidationError(string message) : this(message, 0, 0)
		{
		}

	    public ValidationError(string message, int lineNumber, int linePosition)
	    {
	        Message = message;
	        LineNumber = lineNumber;
	        LinePosition = linePosition;
	    }

	    public string Message { get; private set; }

	    public int LineNumber { get; set; }

	    public int LinePosition { get; set; }

	    public override string ToString()
	    {
	        return Message;
	    }
	}
}