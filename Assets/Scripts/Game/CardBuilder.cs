using System.Collections.Generic;
using UnityEngine;

namespace Cards.Game
{
    public sealed class CardBuilder : MonoBehaviour
    {
        [SerializeField] private TextureCache _textureCache = default;
        [SerializeField] private Card _cardPrefab = default;
        [SerializeField] private Table _table = default;

        [SerializeField] [Range(0,10)] private int _damageMax = 0;
        [SerializeField] [Range(0,10)] private int _costMax = 0;
        [SerializeField] [Range(0,10)] private int _hpMax = 0;

        public Card GetRandomCard()
        {

            Card card = Instantiate(_cardPrefab);
            card.SetData(GetRandomData());
            card.SetTable(_table);

            return card;
        }

        private CardData GetRandomData()
        {
            CardData data = new CardData(GetRandomName(), GetRandomDescription());
            
            var damage = Random.Range(0, _damageMax);
            var cost = Random.Range(0, _costMax);
            var hp = Random.Range(0, _hpMax);
            int id = Random.Range(0, 10);

            data.Damage.SetValue(damage);
            data.Cost.SetValue(cost);
            data.HP.SetValue(hp);

            _textureCache.GetTexture(id, data.CardImage.SetValue);

            return data;
        }

        private string GetRandomName()
        {
            return "Sample Sample";
        }

        private string GetRandomDescription()
        {
            return "Do nothing x4 times. After 3 turns do nothing again.";
        }
    }
}