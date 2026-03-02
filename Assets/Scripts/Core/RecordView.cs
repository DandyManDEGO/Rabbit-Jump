using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Game.Core
{
    public class RecordView : MonoBehaviour
    {
        [Header("Score Text Slots")]
        [SerializeField] private RectTransform[] _scoreTexts;
        [Header("Date Text Slots")]
        [SerializeField] private RectTransform[] _dateTexts;
        private Vector2[] _originalScorePos;
        private Vector2[] _originalDatePos;
        private float _offscreenWidth = 800f;
        private void Awake()
        {
            _originalScorePos = new Vector2[_scoreTexts.Length];
            _originalDatePos = new Vector2[_dateTexts.Length];
            for (int i = 0; i < _scoreTexts.Length; i++)
            {
                _originalScorePos[i] = _scoreTexts[i].anchoredPosition;
                _originalDatePos[i] = _dateTexts[i].anchoredPosition;
            }
        }
        public void DisplayRecords(RecordList records)
        {
            for (int i = 0; i < _scoreTexts.Length; i++)
            {
                _scoreTexts[i].GetComponent<Text>().text = "0";
                _dateTexts[i].GetComponent<Text>().text = "--:--";
            }
            if (records == null || records.Items == null) return;
            for (int i = 0; i < records.Items.Count && i < _scoreTexts.Length; i++)
            {
                _scoreTexts[i].GetComponent<Text>().text = records.Items[i].Score.ToString();
                _dateTexts[i].GetComponent<Text>().text = records.Items[i].Date;
            }
        }
        public void AnimateRecords()
        {
            for (int i = 0; i < _scoreTexts.Length; i++)
            {
                float side = (i % 2 == 0) ? -_offscreenWidth : _offscreenWidth;
                _scoreTexts[i].anchoredPosition = new Vector2(side, _originalScorePos[i].y);
                _dateTexts[i].anchoredPosition = new Vector2(side, _originalDatePos[i].y);
                float delay = i * 0.1f;
                _scoreTexts[i].DOAnchorPos(_originalScorePos[i], 0.6f).SetEase(Ease.OutBack).SetDelay(delay);
                _dateTexts[i].DOAnchorPos(_originalDatePos[i], 0.6f).SetEase(Ease.OutBack).SetDelay(delay);
            }
        }
    }
}

