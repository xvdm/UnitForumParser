using System.Globalization;

namespace AdminBlazor.CustomConstraints;

public sealed class UlongRouteConstraint: IRouteConstraint
{
    public const string UlongRouteConstraintName = "UlongConstraint";
    
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        ArgumentNullException.ThrowIfNull(routeKey);
        ArgumentNullException.ThrowIfNull(values);

        if (!values.TryGetValue(routeKey, out var routeValue) || routeValue == null)
        {
            return false;
        }
        
        if (routeValue is ulong)
        {
            return true;
        }

        var valueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
        return ulong.TryParse(valueString, out _);
    }
}