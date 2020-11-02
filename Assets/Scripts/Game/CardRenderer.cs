using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Cards.Game
{
    public class CardRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer = default;
        
        [SerializeField] private TextMesh _nameText = default;
        [SerializeField] private TextMeshPro _descriptionText = default;

        [SerializeField] private CounterUI _damageCounter = default;
        [SerializeField] private CounterUI _costCounter = default;
        [SerializeField] private CounterUI _hpCounter = default;

        public void SetData(CardData data)
        {
            _nameText.text = data.Name;
            _descriptionText.text = data.Description;
        
            data.HP.Subscribe(_hpCounter.HandleNewValue);
            data.Cost.Subscribe(_costCounter.HandleNewValue);
            data.Damage.Subscribe(_damageCounter.HandleNewValue);
            data.CardImage.Subscribe(HandleCardImage);
        }

        public void SetShining(bool active)
        {

        }

        private void HandleCardImage(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }    
}