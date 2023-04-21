using System.Text.Json;

namespace JsonConfigurationLoader
{
    public class JsonConfigurationLoader
    {
        private readonly JsonElement _config;

        public JsonConfigurationLoader(string configFilePath)
        {
            if (string.IsNullOrEmpty(configFilePath))
            {
                throw new ArgumentException("Configuration file path cannot be null or empty.", nameof(configFilePath));
            }

            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Configuration file not found.", configFilePath);
            }

            string configContent = File.ReadAllText(configFilePath);
            _config = JsonSerializer.Deserialize<JsonElement>(configContent);
        }

        public T GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            if (_config.TryGetProperty(key, out JsonElement value))
            {
                return JsonSerializer.Deserialize<T>(value.GetRawText());
            }

            throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
        }
    }
}