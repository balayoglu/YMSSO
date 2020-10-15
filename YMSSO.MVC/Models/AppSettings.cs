namespace YMDiscourseSSO.Models
{
	public class AppSettings
    {
		public string YmApiKeyPublic { get; set; }
		public string YmApiKeySA { get; set; }
		public string YmApiSAPasscode { get; set; }
		public string YmApiEndpoint { get; set; }
		public string YmApiVersion { get; set; }
		public string YmApiCallOrigin { get; set; }
		public string DiscourseSsoLoginUrl { get; set; }
		public string DiscourseSsoSecret { get; set; }
	}
}