using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

public class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-API-Key";
    private const string ApiKey = "9daeee2f-11c4-4381-a18e-aeb0a7c03a24"; // Replace with your actual API key

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check if the API key is present in the request header
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Check if the provided API key matches the expected one
        if (!string.Equals(extractedApiKey, ApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
