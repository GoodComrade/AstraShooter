using System.Collections.Generic;
using UnityEngine;

// Хранилище данных, использующее одну из реализаций Синглтона.
// Обьект используется для хранения и получения данных в процессе работы игры.
public class DataStorage : PersistentLazySingleton<DataStorage>
{
    // Ссылка на все хранимые данные
    private Dictionary<string, object> storage = new Dictionary<string, object>();

    

    /// <summary>
    /// Метод, используемый для записи в хранилище
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="data">Данные</param>
    public void SaveData(string key, object data)
    {
        if (storage.ContainsKey(key)) // Если что-то с таким же ключом уже присутствует, пишем ошибку.
        {
            Debug.LogWarningFormat("[{0}] Overriding value in: {1}.", typeof(DataStorage), key);
        }

        storage[key] = data;
    }

    /// <summary>
    /// Метод, используемый для проверки, если хранилище имеет данные под заданным ключом.
    /// Возвращает true, если хранилище содержит данные, false, если наоборот.
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <param name="key">Ключ</param>
    /// <returns></returns>
    public bool HasData<T>(string key)
    {
        if (!storage.ContainsKey(key)) 
        {
            return false;
        }

        return ((T)storage[key]) != null; 
    }

    /// <summary>
    /// Метод, используемый для получения данных из хранилища
    /// Возвращает данные
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <param name="key">ключ</param>
    /// <returns>Данные</returns>
    public T GetData<T>(string key)
    {
        if (!storage.ContainsKey(key)) // Проверка, если хранилище имеет данные под указанным ключом.
        {
            Debug.LogWarningFormat("[{0}] No value under key: {1}. Returning default", typeof(DataStorage), key);
            return default(T); // Возвращает стандартное значение типа.
        }

        return (T)storage[key];
    }

    /// <summary>
    /// Метод, используемый для удаления данных из хранилища
    /// </summary>
    /// <param name="key">Ключ</param>
    public void RemoveData(string key)
    {
        if (storage.ContainsKey(key)) // Если данные под указанным ключом существуют, мы удаляем их.
        {
            storage.Remove(key);
        }
    }
}

