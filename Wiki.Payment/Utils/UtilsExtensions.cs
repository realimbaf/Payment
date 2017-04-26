using System;
using System.Web;

namespace Wiki.Payment.Utils
{
    public static class UtilsExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var ub = new UriBuilder(uri);
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Add(name, value);
            ub.Query = httpValueCollection.ToString();
            return ub.Uri;
        }
    }
}
