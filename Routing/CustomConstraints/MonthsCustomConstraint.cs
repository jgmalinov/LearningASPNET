
using System.Text.RegularExpressions;

namespace Routing.CustomConstraints
{
    public class MonthsCustomConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(!values.ContainsKey(routeKey))
            {
                return false; // If the route value is not present, do not match
            }
            string monthValue = values[routeKey]?.ToString()!;
            Regex rg = new Regex("^(oct|nov|dec)$");
            if (rg.IsMatch(monthValue.ToLower()))
            {
                return true; // Match if the month is oct, nov, or dec
            }
            else
            {
                return false; // Do not match for other values
            }
        }
    }
}
