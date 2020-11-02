using System.Collections.Generic;
using UnityEngine;

namespace Cards.Game
{
    public sealed class Hand : MonoBehaviour
    {
        public List<Card> Cards => _cards;

        [SerializeField] private float _arcOriginY = -10;
        [SerializeField] private float _arcStepAngle = 15;
        [SerializeField] private CardBuilder _builder = default;

        private List<Card> _cards = new List<Card>();

        private void Awake()
        {
            for (int i = 0; i < 6; i++)
            {
                CreateCard();
            }
        }

        public void AddCard(Card card)
        {
            card.SetCallbacks(onPickedUp: RemoveCard, onDropped: AddCard);
            card.transform.SetParent(transform);
            _cards.Add(card);

            RepositionCards();
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
            RepositionCards();
        }

        public void CreateCard()
        {
            AddCard(_builder.GetRandomCard());
        }

        private void RepositionCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                var angle = GetArcPosition(i, _cards.Count);
                var x = -Mathf.Sin(angle * Mathf.PI / 180f) * 4;
                var y =  Mathf.Cos(angle * Mathf.PI / 180f) * 2;
                _cards[i].SetPosition(x, y, -i, angle);
            }
        }

        private float GetArcPosition(int index, int length)
        {
            return (-index + (length - 1) / 2f) * _arcStepAngle;
        }
    }
}