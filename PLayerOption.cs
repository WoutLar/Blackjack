
    namespace Blackjack
    {
        using System.Collections.Generic;

        public abstract class PlayerOptions
        {
            public List<Card> Hand { get; } = new List<Card>();
            public int Score { get; protected set; } = 0;

            public void AddCardToHand(Card card)
            {
                Hand.Add(card);
                UpdateScore();
            }

            protected abstract void UpdateScore();
        }


    }