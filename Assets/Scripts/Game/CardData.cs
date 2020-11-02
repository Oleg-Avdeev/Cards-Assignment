using DataBinding;
using UnityEngine;

namespace Cards.Game
{
    public sealed class CardData
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Observable<Sprite> CardImage { get; private set; }

        public Observable<int> Damage { get; private set; }
        public Observable<int> Cost   { get; private set; }
        public Observable<int> HP     { get; private set; }


        public CardData(string name, string description)
        {
            Name = name;
            Description = description;

            CardImage = new Observable<Sprite>();
            
            Damage = new Observable<int>();
            Cost = new Observable<int>();
            HP = new Observable<int>();
        }

        public void Dispose()
        {
            CardImage.ClearSubscribtions();
            Damage.ClearSubscribtions();
            Cost.ClearSubscribtions();
            HP.ClearSubscribtions();
        }
    }
}