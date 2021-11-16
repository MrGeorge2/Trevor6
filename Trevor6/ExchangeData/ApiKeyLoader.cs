using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Trevor6.ExchangeData;
public class ApiKeyLoader
{
    private readonly string _path;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApiKeyLoader(string path)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _path = path;
        load();
    }

    public string Key { get; private set; }

    public string Secret { get; private set; }

    /// <summary>
    /// Load api file
    /// </summary>
    /// <exception cref="Exception"></exception>
    void load()
    {
        if (string.IsNullOrEmpty(_path))
            throw new Exception("File need to be filled");

        if (!File.Exists(_path))
            throw new Exception($"File <{_path}> does not exist");

        var text = File.ReadAllText(_path);

        if (string.IsNullOrEmpty(text))
            throw new Exception("File is empty");

        dynamic? apiKeyLoader = JsonConvert.DeserializeObject(text);
        
        if (apiKeyLoader == null)
            throw new Exception($"Could not deserialize {_path}");

        if (string.IsNullOrEmpty(apiKeyLoader.Secret?.ToString()) || string.IsNullOrEmpty(apiKeyLoader.Key?.ToString()))
            throw new Exception($"File have to be filled");
        
        Key = apiKeyLoader.Key.ToString();
        Secret = apiKeyLoader.Secret.ToString();
    }
}

