﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using TheProxor.AbTest.Connection;

namespace TheProxor.AbTest
{
    public sealed class AbTestDataManager<T> 
    {
        private const string configName = "AbTestConfig.json";

        private string path = Path.Combine(Application.persistentDataPath, configName);

        public T Config { get; private set; }

        public void Initialize(string host, string password)
        {
            AbTestResponseHandler.Initialize(host, Application.productName, password);
        }

        public T Load()
        {
            T result = default;

            string jsonString = string.Empty;

            if(File.Exists(path))
            {
                jsonString = LoadConfigFromFile(path);
            }
            else
            {
                jsonString = AbTestResponseHandler.Get();

                if (string.IsNullOrEmpty(jsonString))
                {
                    Debug.LogError("AbTest Config  is not found");
                    CreateConfig(path, result);
                }
                else
                {
                    result = GetConfigFromJSON(jsonString);
                }
            }

            return result;
        }

        public T LoadAsync()
        {
            T result = default;

            string jsonString = string.Empty;

            if (File.Exists(path))
            {
                jsonString = LoadConfigFromFile(path);
            }
            else
            {
                jsonString = AbTestResponseHandler.GetAsync().Result;

                if (string.IsNullOrEmpty(jsonString))
                {
                    Debug.LogError("AbTest Config  is not found");
                    CreateConfig(path, result);
                }
                else
                {
                    result = GetConfigFromJSON(jsonString);
                }
            }

            return result;
        }

        public T GetConfigFromJSON(string jsonString) => JsonUtility.FromJson<T>(jsonString);
        
        public string LoadConfigFromFile(string path) => File.ReadAllText(path);

        public void CreateConfig(string path, T config)
        {
            File.WriteAllText(path, JsonUtility.ToJson(config));
            Debug.Log($"Config was created with path: {path}");
        }
    }
}