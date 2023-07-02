using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Common.Data
{
    public class DataService
    {
        private JsonSerializer m_JsonSerializer;

        public DataService()
        {
            m_JsonSerializer = new JsonSerializer();
        }

        public void Save<T>(T data) where T : new()
        {
            var filePath = GetPath<T>();
            using var streamWriter = new StreamWriter(filePath);
            streamWriter.AutoFlush = true;
            m_JsonSerializer.Serialize(streamWriter, data);
        }

        public T Load<T>() where T : new()
        {
            var filePath = GetPath<T>();
            T data;
            if (File.Exists(filePath))
            {
                try
                {
                    var fileStream = File.OpenRead(filePath);
                    using var streamReader = new StreamReader(fileStream);
                    using var jsonTextReader = new JsonTextReader(streamReader);
                    data = m_JsonSerializer.Deserialize<T>(jsonTextReader);

                    if (!EqualityComparer<T>.Default.Equals(data, default))
                    {
                        return data;
                    }
                }
                catch (JsonReaderException exception)
                {
                    //To Do advanced Debug log
                    Remove<T>();
                }
            }

            data = new T();
            return data;
        }

        public void Remove<T>() where T : new()
        {
            var filePath = GetPath<T>();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetPath<T>()
        {
            return $"{Application.persistentDataPath}/{typeof(T).Name}.txt";
        }
    }
}