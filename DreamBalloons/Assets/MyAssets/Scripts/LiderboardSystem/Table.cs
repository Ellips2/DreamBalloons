using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace LiderboardSystem
{
    [Serializable]
    public class Table
    {
        [SerializeField] private List<Result> results;

        public Table()
        {
            results = new List<Result>();
        }

        public IEnumerable<Result> Results => results;

        public override string ToString()
        {
            int lenght = results.Count;
            int numberCount = (int)Mathf.Floor(Mathf.Log10(lenght));
            string indexFormat = "D" + numberCount;
            var rows = results.Select((r, i) => string.Format("{0} {1}", i.ToString(indexFormat), r.ToString()));
            return string.Join(Environment.NewLine, rows);
        }

        public Result AddResult(string name, int score)
        {
            var newResult = new Result(name, score);
            results.Add(newResult);
            Sort();
            return newResult;
        }

        public void Crop(int lenght)
        {
            if (results.Count > lenght)
                results.RemoveRange(lenght, results.Count - lenght);
        }

        public void Clean()
        {
            results.Clear();
        }

        private void Sort()
        {
            results.Sort((x, y) => y.Score.CompareTo(x.Score));
        }
    }
}