namespace UmbracoWebApi.Models
{
	public class ResponseObject<T>
	{
		public string statusCode { get; set; }
		public string errorCode { get; set; }
		public string message { get; set; }
		public T response { get; set; }
	}
}
