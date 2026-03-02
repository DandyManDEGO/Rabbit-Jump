using UnityEngine;
using DG.Tweening;
namespace Game.Core
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _buttons;
        public void AnimateAppearance()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (_buttons[i] != null)
                {
                    _buttons[i].localScale = Vector3.zero;
                    _buttons[i].DOScale(1f, 0.6f).SetEase(Ease.OutBack).SetDelay(0.2f + (i * 0.15f));
                }
            }
        }
    }
}

