using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Net;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Collections.Specialized;
using System.IO;
using System.Web.SessionState;
using System.Reflection;
using CardShop.Utilities;

namespace CardShopTest.ControllerTests
{
    public static class ControllerUtility
    {
        public static Controller mockRequest(Type type)
        {
            return mockRequestWithNVC(type, new System.Collections.Specialized.NameValueCollection() { 
                        { "HTTP_REFERER", "www.some_url.other" },
                        { "HTTP_X_FORWARDED_FOR", "iamanaddress" },
                        { "REMOTE_ADDR", "remoteAddress" }
                });
        }

        // making these public static means tests can configure them any time, it also means that these will retain their value between tests,
        // make sure they are reset if you change them.
        public static Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
        public static Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();

        /// <summary>
        /// The properties of the controller are added as public static attributes so that tests can edit them and setup whatever they
        /// need for their tests. all of the setups used in the mockRequestWithNVC are verifyable so that tests can verify anything further.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Controller mockRequestWithNVC(Type type, NameValueCollection collection)
        {
            // First setup request with X-Requested-With header set
            httpRequest.SetupGet(x => x.Headers).Returns(
                        new WebHeaderCollection() { { "X-Requested-With", "XMLHttpRequest" } }).Verifiable();
            httpRequest.Setup(x => x.ServerVariables).Returns(collection).Verifiable();


            // Then config contextBase using above request
            httpContext.SetupGet(x => x.Request).Returns(httpRequest.Object).Verifiable();
            setSession();

            // Set controllerContext
            Controller controller = (Controller)Activator.CreateInstance(type);
            controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
            return controller;
        }

        public static void setSession()
        {

            // set the session variable
            var httpRequest = new HttpRequest("", "http://www.localhost.com/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                        new HttpStaticObjectsCollection(), 10, true,
                        HttpCookieMode.AutoDetect,
                        SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                        BindingFlags.NonPublic | BindingFlags.Instance,
                        null, CallingConventions.Standard,
                        new[] { typeof(HttpSessionStateContainer) },
                        null)
                        .Invoke(new object[] { sessionContainer });

            HttpContext.Current = httpContext;

            SessionObjects.UserID = 1;
        }
    }
}
