using UnityEngine;
using DG.Tweening;

namespace Cards.Game
{
    public sealed class Table : MonoBehaviour
    {
        private bool _isHovering;
        
        public bool IsHovered()
        {
            return _isHovering;
        }

        void OnMouseEnter()
        {
            _isHovering = true;
        }

        void OnMouseExit()
        {
            _isHovering = false;
        }
    }
}