using BdGeographicalData.Shared.AppSettings;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Net.Http.Headers;

namespace BdGeographicalData.Shared;

public class ResponseCacheConfigMiddleware(IAppSettingsDataResolver appSettingsDataResolver) : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.GetTypedHeaders().CacheControl =
            new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(appSettingsDataResolver.Resolve().ResponseCacheDurationInSecond)
            };

        var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
        if (responseCachingFeature is not null)
            responseCachingFeature.VaryByQueryKeys = new[] { "*" };

        return next(context);
    }
}