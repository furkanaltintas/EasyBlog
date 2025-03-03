using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace EasyBlog.Service.Aspects;

public class CacheAspect : IInterceptor
{
    private readonly IMemoryCache _cache;

    public CacheAspect(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Intercept(IInvocation invocation)
    {
        var methodName = invocation.Method.Name;
        var cacheKey = $"{invocation.TargetType.FullName}.{methodName}({string.Join(",", invocation.Arguments)})";

        // **GET işlemlerini cache'den getir methodName.StartsWith("Get") **
        if (invocation.Method.IsPublic && invocation.Method.GetCustomAttributes(typeof(HttpPostAttribute), true).Length != 0)
        {
            if (_cache.TryGetValue(cacheKey, out var cachedResult))
            {
                invocation.ReturnValue = cachedResult;
                return;
            }

            invocation.Proceed(); // Metodu çalıştır
            _cache.Set(cacheKey, invocation.ReturnValue, TimeSpan.FromMinutes(10));
        }
        else // **Add, Update veya Delete işlemlerinde cache temizle**
        {
            invocation.Proceed(); // Metodu çalıştır

            var cacheKeys = _cache.GetType()
                .GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(_cache) as dynamic;

            if (cacheKeys != null)
            {
                foreach (var cacheEntry in cacheKeys)
                {
                    _cache.Remove(cacheEntry.Key);
                }
            }
        }
    }
}