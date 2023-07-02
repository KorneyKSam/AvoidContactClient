using System.IO;
using Newtonsoft.Json;

public class DataSerializator
{
    private JsonSerializer m_JsonSerializer;

    public DataSerializator()
    {
        m_JsonSerializer = new JsonSerializer();
    }

    public void Save<T>(T data, string filePath)
    {
        using var streamWriter = new StreamWriter(filePath);
        streamWriter.AutoFlush = true;
        m_JsonSerializer.Serialize(streamWriter, data);
    }

    public T Load<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            var fileStream = File.OpenRead(filePath);
            using var streamReader = new StreamReader(fileStream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            return m_JsonSerializer.Deserialize<T>(jsonTextReader);
        }
        else
        {
            return default(T);
        }
    }

    public void Reset(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}