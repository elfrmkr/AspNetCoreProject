using System.Text.RegularExpressions;

namespace AspNetCoreProject.CustomConstraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // check whether value exist
            if (!values.ContainsKey(routeKey))
            {
                return false; // not a match
            }
            Regex regex = new Regex("^(apr|jul|oct|jan)$");
            string? monthValue = Convert.ToString(values[routeKey]);
            if(regex.IsMatch(monthValue))
            {
                return true;
            }
            return false;
        }
    }
}
