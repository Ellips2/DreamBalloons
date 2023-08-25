using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace LiderboardSystem
{
    [Serializable]
    public class Table
    {
        [SerializeField] private List<Result> _results;

        public Table()
        {
            _results = new List<Result>();
        }

        public IEnumerable<Result> Results => _results;

        public override string ToString()
        {
            int lenght = _results.Count;
            int numberCount = (int)Mathf.Floor(Mathf.Log10(lenght));
            string indexFormat = "D" + numberCount;
            var rows = _results.Select((r, i) => string.Format("{0} {1}", i.ToString(indexFormat), r.ToString()));
            return string.Join(Environment.NewLine, rows);
        }

        public Result AddResult(string name, int score)
        {
            var newResult = new Result(name, score);
            _results.Add(newResult);
            Sort();
            return newResult;
        }

        public void Crop(int lenght)
        {
            if (_results.Count > lenght)
                _results.RemoveRange(lenght, _results.Count - lenght);
        }

        public void Clean()
        {
            _results.Clear();
        }

        private void Sort()
        {
            _results.Sort((x, y) => y.Score.CompareTo(x.Score));
        }
    }
}