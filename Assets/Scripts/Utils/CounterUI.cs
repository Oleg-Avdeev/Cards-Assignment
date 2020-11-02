using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace Cards
{
    public sealed class CounterUI : MonoBehaviour
    {
        [SerializeField] private TextMesh _counterText = default;
        [SerializeField] private Transform _counterIcon = default;
        [SerializeField] private Vector3 _punchVector = default;

        private Tween _currentTween;

        public void HandleNewValue(int value)
        {
            _counterText.text = value.ToString();
            
            if (_currentTween != null)
            {
                _currentTween.Kill();
                _counterIcon.localScale = Vector3.one;
            }

            _currentTween = _counterIcon.DOPunchScale(_punchVector, 0.4f, 1);
        }
    }
}