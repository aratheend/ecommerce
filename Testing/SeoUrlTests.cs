using System;
using Web.Helpers;

namespace Testing
{
	public class SeoUrlTests
	{
		[Test]
		public void UpperCase_Seo_Url_Tests()
		{
			var text = "Rəqəmsal sənətçi və qrafik dizayn müəllimi";
			var result = "rəqəmsal-senetci-ve-qrafik-dizayn-muellimi";
			var methodResult = SeoHelper.SeoUrlCreator(text);
            Assert.AreEqual(result,methodResult);
		}

        
    }
}

