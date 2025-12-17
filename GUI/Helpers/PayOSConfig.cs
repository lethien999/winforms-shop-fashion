using System;
using System.IO;
using System.Text.Json;

namespace WinFormsFashionShop.Presentation.Helpers
{
    /// <summary>
    /// Configuration class for PayOS API credentials.
    /// Single responsibility: only stores and loads PayOS configuration.
    /// </summary>
    public static class PayOSConfig
    {
        private static readonly string ConfigFilePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, 
            "Config", 
            "payos.config.json");

        private static string _clientId = string.Empty;
        private static string _apiKey = string.Empty;
        private static string _checksumKey = string.Empty;
        private static bool _isLoaded = false;

        /// <summary>
        /// PayOS Client ID from PayOS dashboard.
        /// </summary>
        public static string ClientId
        {
            get
            {
                if (!_isLoaded)
                    LoadConfig();
                return _clientId;
            }
            set
            {
                _clientId = value;
                SaveConfig();
            }
        }

        /// <summary>
        /// PayOS API Key from PayOS dashboard.
        /// </summary>
        public static string ApiKey
        {
            get
            {
                if (!_isLoaded)
                    LoadConfig();
                return _apiKey;
            }
            set
            {
                _apiKey = value;
                SaveConfig();
            }
        }

        /// <summary>
        /// PayOS Checksum Key from PayOS dashboard.
        /// </summary>
        public static string ChecksumKey
        {
            get
            {
                if (!_isLoaded)
                    LoadConfig();
                return _checksumKey;
            }
            set
            {
                _checksumKey = value;
                SaveConfig();
            }
        }

        /// <summary>
        /// Loads configuration from JSON file.
        /// Single responsibility: only loads configuration.
        /// </summary>
        private static void LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    var json = File.ReadAllText(ConfigFilePath);
                    var config = JsonSerializer.Deserialize<PayOSConfigData>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (config?.PayOS != null)
                    {
                        _clientId = config.PayOS.ClientId ?? string.Empty;
                        _apiKey = config.PayOS.ApiKey ?? string.Empty;
                        _checksumKey = config.PayOS.ChecksumKey ?? string.Empty;
                    }
                }
                else
                {
                    // Không tự set giá trị mặc định (tránh hardcode secret).
                    // Nếu chưa có file cấu hình thì coi như chưa cấu hình PayOS.
                    _clientId = string.Empty;
                    _apiKey = string.Empty;
                    _checksumKey = string.Empty;
                }
                _isLoaded = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading PayOS config: {ex.Message}");
                // Khi lỗi đọc config, coi như chưa cấu hình PayOS.
                _clientId = string.Empty;
                _apiKey = string.Empty;
                _checksumKey = string.Empty;
                _isLoaded = true;
            }
        }

        /// <summary>
        /// Saves configuration to JSON file.
        /// Single responsibility: only saves configuration.
        /// </summary>
        private static void SaveConfig()
        {
            try
            {
                var configDir = Path.GetDirectoryName(ConfigFilePath);
                if (!string.IsNullOrEmpty(configDir) && !Directory.Exists(configDir))
                {
                    Directory.CreateDirectory(configDir);
                }

                var config = new PayOSConfigData
                {
                    PayOS = new PayOSCredentials
                    {
                        ClientId = _clientId,
                        ApiKey = _apiKey,
                        ChecksumKey = _checksumKey
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
                System.Diagnostics.Debug.WriteLine($"Error saving PayOS config: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if PayOS is configured.
        /// Single responsibility: only validates configuration.
        /// </summary>
        public static bool IsConfigured()
        {
            if (!_isLoaded)
                LoadConfig();
            
            return !string.IsNullOrWhiteSpace(_clientId) &&
                   !string.IsNullOrWhiteSpace(_apiKey) &&
                   !string.IsNullOrWhiteSpace(_checksumKey);
        }

        /// <summary>
        /// Reloads configuration from file.
        /// Single responsibility: only reloads configuration.
        /// </summary>
        public static void Reload()
        {
            _isLoaded = false;
            LoadConfig();
        }

        // Private classes for JSON deserialization
        private class PayOSConfigData
        {
            public PayOSCredentials? PayOS { get; set; }
        }

        private class PayOSCredentials
        {
            public string? ClientId { get; set; }
            public string? ApiKey { get; set; }
            public string? ChecksumKey { get; set; }
        }
    }
}

