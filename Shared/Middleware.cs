using BdGeographicalData.Shared.AppSettings;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Net.Http.Headers;

namespace BdGeographicalData.Shared;

public class Middleware(IAppSettingsData appSettingsData)
{
    public readonly Func<HttpContext, Func<Task>, Task> ResponseCache = (context, next) =>
    {
        context.Response.GetTypedHeaders().CacheControl =
            new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(appSettingsData.ResponseCacheDurationInSecond)
            };

        var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
        if (responseCachingFeature is not null)
            responseCachingFeature.VaryByQueryKeys = new[] { "*" };

        return next();
    };
}