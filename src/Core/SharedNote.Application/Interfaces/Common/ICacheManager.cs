
using System;
using System.Threading.Tasks;

namespace SharedNote.Application.Interfaces.Common
{
    public interface ICacheManager
    {
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> func, double? absoluteTime = null);
        void RemoveSameCache(string key);
        void RemoveSpecificKey(string key);
    }
}
