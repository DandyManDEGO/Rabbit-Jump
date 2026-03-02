using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Game.Core
{
    public class RecordService
    {
        private const string SAVE_KEY = "GameRecords";
        private const int MAX_RECORDS = 4;
        public void SaveRecord(int score)
        {
            if (score <= 0) return;
            RecordList records = LoadRecords();
            string currentDate = DateTime.Now.ToString("dd.MM");
            records.Items.Add(new RecordData(score, currentDate));
            records.Items = records.Items
                .OrderByDescending(r => r.Score)
                .Take(MAX_RECORDS)
                .ToList();
            string json = JsonUtility.ToJson(records);
            PlayerPrefs.SetString(SAVE_KEY, json);
            PlayerPrefs.Save();
        }
        public RecordList LoadRecords()
        {
            if (!PlayerPrefs.HasKey(SAVE_KEY)) return new RecordList();
            string json = PlayerPrefs.GetString(SAVE_KEY);
            return JsonUtility.FromJson<RecordList>(json);
        }
    }
}

