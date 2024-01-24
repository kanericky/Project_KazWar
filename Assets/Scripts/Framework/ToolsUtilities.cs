using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class ToolsUtilities
    {
        public static ToolsUtilities Instance = new ToolsUtilities();
        
        public void Shuffle<T>(ref List<T> list)
        {
            int n = list.Count;
            System.Random rng = new System.Random();

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T LoadResource<T>(string path) where T : Component
        {
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError("Failed to load the prefab at path: " + path);
                return null;
            }

            if(prefab.GetComponent<T>())
            {
                return prefab.GetComponent<T>();
            }
            else
            {
                return prefab.AddComponent<T>();
            }
        }
    }
    
}