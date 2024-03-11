using System;

namespace Blackjack
{
    public class Player
    {
        public Hand Hand { get; }
        public int BetAmount { get; private set; } = 0;

        public Player()
        {
            Hand = new Hand();
        }

        public virtual void Hit(Deck deck)
        {
            Card drawnCard = deck.Draw();
            Hand.AddCard(drawnCard);
        }

        public virtual void Stand()
        {
            Console.WriteLine("Player stands.");
        }

        public virtual void DoubleDown(Deck deck)
        {
            BetAmount *= 2;
            Card drawnCard = deck.Draw();
            Hand.AddCard(drawnCard);
            
            Stand();
        }

        public virtual void Split()
        {
            // Implement split logic here
        }
    }
}