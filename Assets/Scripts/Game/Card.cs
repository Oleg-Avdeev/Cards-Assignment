using UnityEngine;
using DG.Tweening;
using System;

namespace Cards.Game
{
    public sealed class Card : DraggableBehaviour
    {
        public CardData GetData() => _cardData;

        [SerializeField] private CardRenderer _cardRenderer = default;

        private Action<Card> _onPickedUp;
        private Action<Card> _onDropped;
        private CardData _cardData;
        private Table _table;

        public void SetCallbacks(Action<Card> onPickedUp, Action<Card> onDropped)
        {
            _onPickedUp = onPickedUp;
            _onDropped = onDropped;
        }

        public void SetTable(Table table)
        {
            _table = table;
        }

        public void SetData(CardData data)
        {
            data.HP.Subscribe(HandleHPChange);

            _cardRenderer.SetData(data);
            _cardData = data;
        }

        public void RemoveCard()
        {
            _onPickedUp?.Invoke(this);
            transform.DOMove(new Vector3(0, -7, 0), 0.4f).OnComplete(Dispose);
        }

        public void SetPosition(float x, float y, float z, float angle)
        {
            transform.DOLocalMove(new Vector3(x, y, z), 0.4f);
            transform.DOLocalRotate(new Vector3(0, 0, angle), 0.4f, RotateMode.Fast);
        }

        protected override void HandlePickedUp()
        {
            _cardRenderer.SetShining(true);
            transform.localRotation = Quaternion.identity;
            _onPickedUp?.Invoke(this);
            base.HandlePickedUp();
        }

        protected override void HandleDropped()
        {
            _cardRenderer.SetShining(false);
            if (!_table.IsHovered())
            {
                _onDropped?.Invoke(this);
            }

            base.HandleDropped();
        }

        private void HandleHPChange(int value)
        {
            if (value < 1)
            {
                RemoveCard();
            }
        }

        private void Dispose()
        {
            gameObject.SetActive(false);
            _cardData.Dispose();

            _onPickedUp = null;
            _onDropped = null;
            _cardData = null;
            _table = null;
        }
    }
}