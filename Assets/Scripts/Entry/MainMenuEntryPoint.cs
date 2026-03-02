using UnityEngine;
using Game.Core;
namespace Game.Entry
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private RecordView _recordView;
        [SerializeField] private MainMenuUI _menuUI;
        private void Start()
        {
            Application.targetFrameRate = 90;
            Time.timeScale = 1f;
            RecordService service = new RecordService();
            RecordList records = service.LoadRecords();
            if (_recordView != null)
            {
                _recordView.DisplayRecords(records);
            }
            if (_menuUI != null)
            {
                _menuUI.AnimateAppearance();
            }
        }
    }
}

