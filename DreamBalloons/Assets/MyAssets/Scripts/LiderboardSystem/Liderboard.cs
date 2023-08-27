using System.Collections.Generic;
using UnityEngine;

namespace LiderboardSystem
{
    [CreateAssetMenu(fileName = "Liderboard", menuName = "Liderboard")]
    public class Liderboard : ScriptableObject
    {
        private const string key = "Liderboard";
        [SerializeField] private int _capacity = 10;
        private Table _table;

        public IEnumerable<Result> Results => _table.Results;

        public void Initialize()
        {
            Load();
        }

        public Result AddResult(string name, int score)
        {
            var result = _table.AddResult(name, score);
            _table.Crop(_capacity);
            Save();
            return result;
        }

        public void Clean()
        {
            _table.Clean();
            Save();
        }

        private void Load()
        {
            string json = PlayerPrefs.GetString(key);
            Debug.Log(json);
            if (string.IsNullOrEmpty(json))
                _table = new Table();
            else
                _table = JsonUtility.FromJson<Table>(json);
        }

        private void Save()
        {
            string json = JsonUtility.ToJson(_table);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}