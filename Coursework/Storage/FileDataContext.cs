using System.Text.Json;
using DAL.Exceptions;

namespace DAL.Storage
{
    public class FileStorageSettings
    {
        public string DataFolder { get; set; } = "Data";
    }

    public class FileDataContext
    {
        private readonly FileStorageSettings _settings;

        public FileDataContext(FileStorageSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            if (!Directory.Exists(_settings.DataFolder))
                Directory.CreateDirectory(_settings.DataFolder);
        }

        public List<T> LoadList<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string path = Path.Combine(_settings.DataFolder, fileName);
            if (!File.Exists(path))
                return new List<T>(); 

            try
            {
                string json = File.ReadAllText(path);
                var list = JsonSerializer.Deserialize<List<T>>(json);
                return list ?? new List<T>();
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to load data from file '{fileName}'", ex);
            }
        }

        public void SaveList<T>(string fileName, List<T> items)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string path = Path.Combine(_settings.DataFolder, fileName);

            try
            {
                var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Failed to save data to file '{fileName}'", ex);
            }
        }
    }
}
