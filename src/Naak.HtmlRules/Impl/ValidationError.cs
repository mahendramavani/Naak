namespace Naak.HtmlRules.Impl
{
	public class ValidationError
	{
		public string Message { get; private set; }

		public ValidationError(string message)
		{
			Message = message;
		}

		public override string ToString()
		{
			return Message;
		}
	}
}