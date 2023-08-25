using System;
using System.Globalization;

namespace LiderboardSystem
{
    [Serializable]
    public struct Result
    {
        public string Name;
        public int Score;
        public long DateLong;

        public Result(string name, int score, DateTime date)
        {
            Name = name;
            Score = score;
            DateLong = date.ToFileTimeUtc();
        }

        public Result(string name, int score) : this(name, score, DateTime.Now)
        {
        }

        public DateTime Date => DateTime.FromFileTimeUtc(DateLong);

        public override string ToString()
        {
            var dateStr = Date.ToString("G", CultureInfo.GetCultureInfo("de-DE"));
            return string.Format("{0} {1} {2}", Name, Score, dateStr);
        }
    }
}