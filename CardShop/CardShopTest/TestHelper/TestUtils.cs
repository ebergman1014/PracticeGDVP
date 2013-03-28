using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace CardShopTest.TestHelper
{
    [TestClass]
    public class TestUtils
    {
        internal static AuthorizationContext CreateAuthorizationContext()
        {
            AuthorizationContext context = new AuthorizationContext();
            context.HttpContext = new HttpContextWrapper(CreateHttpContext());
            return context;
        }

        internal static HttpContext CreateHttpContext()
        {
            HttpRequest request = new HttpRequest("", "http://localhost/", "");
            HttpResponse response = new HttpResponse(new StringWriter());
            return new HttpContext(request, response);
        }
    }
}
