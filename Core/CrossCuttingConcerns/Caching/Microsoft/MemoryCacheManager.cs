using Core.Utilities.IOC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern var olan bir sistemi kendi sistemimize göre uyarlıyoruz
        IMemoryCache _memoryCahce;//microsoft a ait 
        public MemoryCacheManager()
        {
            _memoryCahce=ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCahce.Set(key,value,TimeSpan.FromMinutes(duration));
            //ne kadar süre verirsen o kadar cache de kalacak
        }

        public T Get<T>(string key)
        {
            return _memoryCahce.Get<T>(key);
        }

        public object Get(string key)
        {
           return _memoryCahce.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCahce.TryGetValue(key,out _);
            //sadece bellekte böyle key varmı diye bakıyoruz value verme diyoruz
        }

        public void Remove(string key)
        {
            _memoryCahce.Remove(key);
        }
        public void RemoveByPattern(string pattern)
        {
            //RemoveByPattern bellekten silmeye yarıyor çalışma anında
            //reflection ile çalışma anında elimizde bulunan nesnelere 


            //git belleğe bak bellekte memoryCache türünde olan EntriesCollection cache datalarını EntriesCollectionun içine atıyor git EntriesCollectionu bul
            
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //definitionu _memoryCache olanı bul
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCahce) as dynamic;
            //onları listeye koy
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            //onların her birini gez
            foreach (var cacheItem in cacheEntriesCollection)
            {
                //
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }
            //her bir cache elemanınadan bu kuralara uyanlar  
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);//pattren böyle oluşturuyoruz
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            //uygun değerler varsa onları keysToRemove içine alıp foreach ile gez ver sil
            foreach (var key in keysToRemove)
            {
                _memoryCahce.Remove(key);
            }
        }
    }
}
