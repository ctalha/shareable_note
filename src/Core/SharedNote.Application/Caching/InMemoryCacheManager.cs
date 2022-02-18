using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SharedNote.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedNote.Application.Caching
{
    public class InMemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        public IConfiguration _configuration { get; }
        private readonly static double _absoluteExpiration = 240;

        private MemoryCacheEntryOptions _options = new()
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(_absoluteExpiration),
            Priority = CacheItemPriority.Normal
        };
        public InMemoryCacheManager(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
           // _absoluteExpiration = configuration;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> func, double? absoluteTime = null)
        {
            T data;
            if (_memoryCache.TryGetValue(key, out data))
                return data;

            var result = await func.Invoke();

            if (absoluteTime != null)
                _options.AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteTime.Value);

            _memoryCache.Set(key, result,_options);

            if (result == null)
                _memoryCache.Remove(key);
            return result;
        }

        public void RemoveSameCache(string key)
        {
            RemoveRegexPattern(key);
        }
        public void RemoveSpecificKey(string key)
        {
            _memoryCache.Remove(key);
        }

        private void RemoveRegexPattern(string key)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex('^' + key, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var item in keysToRemove)
            {
                _memoryCache.Remove(item);
            }
        }

    }
}
