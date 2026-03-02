using System;
namespace Game.Core
{
    [Serializable]
    public class RecordData
    {
        public int Score;
        public string Date;
        public RecordData(int score, string date)
        {
            Score = score;
            Date = date;
        }
    }
    [Serializable]
    public class RecordList
    {
        public System.Collections.Generic.List<RecordData> Items = new System.Collections.Generic.List<RecordData>();
    }
}

