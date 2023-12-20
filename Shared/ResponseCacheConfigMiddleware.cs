using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace BdGeographicalData.Shared;

public class ResponseCacheConfigMiddleware(IOptions<AppOptions> options) : IMiddleware
{
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.GetTypedHeaders().CacheControl =
            new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(options.Value.ResponseCacheDurationInSecond)
            };

        var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();
        if (responseCachingFeature is not null)
            responseCachingFeature.VaryByQueryKeys = new[] { "*" };
        return next(context);
    }
}