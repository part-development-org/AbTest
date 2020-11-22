using System.IO;
using UnityEngine;
using TheProxor.AbTest.Connection;

namespace TheProxor.AbTest
{
    public sealed class AbTestDataManager<T> where T : new()
    {
        private const string configName = "AbTestConfig.json";

        #if UNITY_EDITOR
        private string path = Path.Combine(Application.dataPath, configName);
        #else
        private string path = Path.Combine(Application.persistentDataPath, configName);
        #endif


        public T Config { get; private set; }

        public void Initialize(string host, string password)
        {
            AbTestResponseHandler.Initialize(host, Application.productName, password);
        }

        public T Load()
        {
            T result = new T();

            string jsonString = string.Empty;

            if (File.Exists(path))
            {
                jsonString = LoadConfigFromFile(path);
            }
            else
            {
                jsonString = AbTestResponseHandler.Get();
            }

            if (string.IsNullOrEmpty(jsonString))
            {
                Debug.LogError("AbTest Config  is not found");
            }
            else
            {
                result = GetConfigFromJSON(jsonString);
            }

            RecreateConfig(path, result);

            return result;
        }

        public T LoadAsync()
        {
            T result = new T();

            string jsonString = string.Empty;

            if (File.Exists(path))
            {
                jsonString = LoadConfigFromFile(path);  
            }
            else
            {
                jsonString = AbTestResponseHandler.GetAsync().Result;
            }

            if (string.IsNullOrEmpty(jsonString))
            {
                Debug.LogError("AbTest Config  is not found");
            }
            else
            {
                result = GetConfigFromJSON(jsonString);
            }
           
            RecreateConfig(path, result);

            return result;
        }

        public T GetConfigFromJSON(string jsonString) => JsonUtility.FromJson<T>(jsonString);
        
        public string LoadConfigFromFile(string path) => File.ReadAllText(path);

        public void RecreateConfig(string path, T config)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonUtility.ToJson(config, true));
            Debug.Log($"Config was created with path: {path}");
        }
    }
}
