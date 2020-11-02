#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Cards.Game
{
    [CustomEditor(typeof(Hand))]
    public class HandEditor : Editor 
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var hand = ((Hand)target);

            if (GUILayout.Button("Create Card")) hand.CreateCard();
        }
    }
}
#endif