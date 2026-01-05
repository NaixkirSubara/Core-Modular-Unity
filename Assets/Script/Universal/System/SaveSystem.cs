using System.IO;
using UnityEngine;

namespace MyStudio.Core.Systems
{
    public static class SaveSystem
    {
        private static string BasePath => Application.persistentDataPath;

        public static void Save<T>(T data, string fileName)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(BasePath, fileName);
            File.WriteAllText(path, json);
            
            Debug.Log($"[SaveSystem] Data saved to: {path}");
        }

        public static T Load<T>(string fileName)
        {
            string path = Path.Combine(BasePath, fileName);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }
            else
            {
                Debug.LogWarning($"[SaveSystem] File not found: {path}");
                return default(T); 
            }
        }
        
        public static void DeleteSave(string fileName)
        {
            string path = Path.Combine(BasePath, fileName);
            if (File.Exists(path)) File.Delete(path);
        }
    }
}