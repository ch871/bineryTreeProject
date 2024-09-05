using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace bineryTreeProject
{
    internal class jsonTools
    {
        public static async Task WriteToJsonFileAsync<T>(string filePath, T data, JsonSerializerOptions options = null)
        {
            options ??= new JsonSerializerOptions { WriteIndented = true };
            await using var stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, data, options);
        }
        public static T? ReadFromJsonAsync<T>(string filePath, JsonSerializerOptions options = null)
        {
            try
            {
                options ??= new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                T? data = JsonSerializer.Deserialize<T>(
                    File.OpenRead(filePath),
                    options
                );
                return data;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
