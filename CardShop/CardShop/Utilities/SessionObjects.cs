using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CardShop.Utilities
{
    public class SessionObjects
    {
        private static HttpSessionState session = HttpContext.Current.Session;

        public static int UserID
        {
            get { return (session["UserID"] != null && (session["UserID"].GetType() == typeof(System.Int32)) ? (int)session["UserID"] : -1); }
            set { session["UserID"] = value; }
        }

        public static int ActingAsId
        {
            get { return (session["__UserAuth"] != null && (session["__UserAuth"].GetType() == typeof(System.Int32)) ? (int)session["__UserAuth"] : -1); }
            set { session["__UserAuth"] = value; }
        }

        public static void ClearSession()
        {
            session.Clear();
        }
    }
}