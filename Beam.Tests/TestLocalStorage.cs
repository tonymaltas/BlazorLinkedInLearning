using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Collections.Generic;

namespace Beam.Tests
{
    class TestLocalStorage : ILocalStorageService
    {
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public event EventHandler<ChangingEventArgs> Changing;
        public event EventHandler<ChangedEventArgs> Changed;

        public ValueTask ClearAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> ContainKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public ValueTask<string> GetItemAsStringAsync(string key)
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> GetItemAsync<T>(string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return new ValueTask<T>((Task<T>)dictionary[key]);
            }
            else
            {
                return default;
            }
        }

        public ValueTask<string> KeyAsync(int index)
        {
            throw new NotImplementedException();
        }

        public ValueTask<int> LengthAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask RemoveItemAsync(string key)
        {
            throw new NotImplementedException();
        }

        public ValueTask SetItemAsync<T>(string key, T data)
        {
            Changing?.Invoke(this, new ChangingEventArgs() { Key = key, NewValue = data});
            dictionary[key] = data;
            Changed?.Invoke(this, new ChangedEventArgs() { Key = key, NewValue = data });
            return default;
        }
    }
}