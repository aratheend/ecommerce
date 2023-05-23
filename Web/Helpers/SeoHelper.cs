using System;
namespace Web.Helpers
{
	public class SeoHelper
	{
		public static string SeoUrlCreator(string contentTitle)
		{
			var result = contentTitle
				.ToLower()
				.Replace(" ","-")
				.Replace("ə", "e")
				.Replace("ı", "i")
				.Replace(".", "");

            return result;
		}
	}
}

