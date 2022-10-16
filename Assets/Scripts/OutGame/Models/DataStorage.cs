using System.Collections.Generic;
using UnityEngine;

// Data storage that uses one of the singleton implementations.
// Object is used to store and get data through game life.
public class DataStorage : PersistentLazySingleton<DataStorage>
{
    // References to all stored data
    private Dictionary<string, object> storage = new Dictionary<string, object>();

    // Method used to save data in storage.
    /// <param name="key">Key.</param>
    /// <param name="data">Data.</param>
    public void SaveData(string key, object data)
    {
        if (storage.ContainsKey(key)) // If something under key exist already we are printing warning.
        {
            Debug.LogWarningFormat("[{0}] Overriding value in: {1}.", typeof(DataStorage), key);
        }

        storage[key] = data;
    }

    // Method used to verify if storage has data under provided key.
    // Return true, if storage contains data, false otherwise.
    /// <param name="key">Key.</param>
    /// <typeparam name="T">Expected data type.</typeparam>
    public bool HasData<T>(string key)
    {
        if (!storage.ContainsKey(key)) // If storage doesn't has key then return false.
        {
            return false;
        }

        return ((T)storage[key]) != null; // If storage has data but we need to verify type.
    }

    // Method used to get data from storage.
    // Return Data.
    /// <param name="key">Key.</param>
    /// <typeparam name="T">Expected data type.</typeparam>
    public T GetData<T>(string key)
    {
        if (!storage.ContainsKey(key)) // Check is storage has data under provided key.
        {
            Debug.LogWarningFormat("[{0}] No value under key: {1}. Returning default", typeof(DataStorage), key);
            return default(T); // Return default value for type.
        }

        return (T)storage[key];
    }

    // Method used to remove data from storage.
    /// <param name="key">Key.</param>
    public void RemoveData(string key)
    {
        if (storage.ContainsKey(key)) // If data under provided key exist, we are removing it.
        {
            storage.Remove(key);
        }
    }
}

