using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.ApplicationService
{
    public interface ICacheService
    {
        void Add<T>(string key, T value);
        T? Get<T>(string key);
        void Remove(string key);
    }
}