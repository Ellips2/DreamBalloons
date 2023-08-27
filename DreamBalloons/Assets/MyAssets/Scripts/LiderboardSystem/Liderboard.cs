using System.Collections.Generic;
using UnityEngine;

namespace LiderboardSystem
{
    [CreateAssetMenu(fileName = "Liderboard", menuName = "Liderboard")]
    public class Liderboard : ScriptableObject
    {
        private const string key = "Liderboard";
        [SerializeField] private int capacity = 10;
        private Table table;

        public IEnumerable<Result> Results => table.Results;

        public void Initialize()
        {
            Load();
        }

        public Result AddResult(string name, int score)
        {
            var result = table.AddResult(name, score);
            table.Crop(capacity);
            Save();
            return result;
        }

        public void Clean()
        {
            table.Clean();
            Save();
        }

        private void Load()
        {
            string json = PlayerPrefs.GetString(key);
            Debug.Log(json);
            if (string.IsNullOrEmpty(json))
                table = new Table();
            else
                table = JsonUtility.FromJson<Table>(json);
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(table);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}