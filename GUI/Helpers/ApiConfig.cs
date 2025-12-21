using System;
using System.IO;
using System.Text.Json;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Configuration class cho Backend API URL
    /// </summary>
    public static class ApiConfig
    {
        private static readonly string ConfigFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Config",
            "api.config.json");

        private static string _baseUrl = "https://localhost:7000";
        private static bool _isLoaded = false;

        /// <summary>
        /// Base URL của Backend API
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                if (!_isLoaded)
                    LoadConfig();
                return _baseUrl;
            }
            set
            {
                _baseUrl = value;
                SaveConfig();
            }
        }

        private static void LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    var config = JsonSerializer.Deserialize<ApiConfigData>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (config?.Api != null && !string.IsNullOrWhiteSpace(config.Api.BaseUrl))
                    {
                        _baseUrl = config.Api.BaseUrl;
                    }
                }
                _isLoaded = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading API config: {ex.Message}");
                _isLoaded = true;
            }
        }

        private static void SaveConfig()
        {
            try
            {
                var configDir = Path.GetDirectoryName(ConfigFilePath);
                if (!string.IsNullOrEmpty(configDir) && !Directory.Exists(configDir))
                {
                    Directory.CreateDirectory(configDir);
                }

                var config = new ApiConfigData
                {
                    Api = new ApiSettings
                    {
                        BaseUrl = _baseUrl
                    }
                };

                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(ConfigFilePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error saving API config: {ex.Message}");
            }
        }

        private class ApiConfigData
        {
            public ApiSettings? Api { get; set; }
        }

        private class ApiSettings
        {
            public string? BaseUrl { get; set; }
        }
    }
}

