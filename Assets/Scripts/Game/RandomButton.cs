using UnityEngine.UI;
using UnityEngine;

namespace Cards.Game
{
    // Changes a random value of a card in your hand
    // Chooses cards sequentially from left to right

    public sealed class RandomButton : MonoBehaviour
    {
        [SerializeField] private Hand _hand = default;

        private int _currentIndex = 0;

        public void HandleClick()
        {
            var cards = _hand.Cards;

            if (cards.Count > 0)
            {
                _currentIndex = (_currentIndex + 1) % cards.Count;
                var data = cards[_currentIndex].GetData();
                GiveRandomEffect(data);
            }
        }

        private void GiveRandomEffect(CardData card) 
        {
            int p = Random.Range(0, 100);
            int value = Random.Range(-2, 9);

            if (p < 33)
            {
                card.Cost.Value = value;
            }
            else if (p < 66)
            {
                card.Damage.Value = value;
            }
            else
            {
                card.HP.Value = value;
            }
        }
    }
}