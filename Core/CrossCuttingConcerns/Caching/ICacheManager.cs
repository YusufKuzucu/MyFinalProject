using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object value, int duration);
        bool IsAdd(string key);//cache de varmı
        void Remove(string key);//cacheden uçurma
        void RemoveByPattern(string pattren);//key içinde olanları sil
    }
}
