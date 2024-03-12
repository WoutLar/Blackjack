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
            Console.WriteLine("you drew : "+drawnCard);
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
        
        public string PlayBasicStrategy(Card dealerUpcard, Deck deck)
        {
            int handTotal = Hand.Score;
            string Action = "stand";
            if (handTotal <= 8)
            {
                Console.WriteLine("hit!");
                Action = "hit";
            }
            else if (handTotal == 9)
            {
                if (dealerUpcard.Rank >= Ranks.THREE && dealerUpcard.Rank <= Ranks.SIX)
                {
                    Console.WriteLine("DOUBLE DOWN!");
                    Action = "DD";
                }
                else
                {
                    Console.WriteLine("hit!");
                    Action = "hit";
                }
            }
            else if (handTotal == 10 || handTotal == 11)
            {
                if (dealerUpcard.Rank == Ranks.ACE || dealerUpcard.Rank >= Ranks.TEN)
                {
                    Console.WriteLine("HIT!");
                    Action = "hit";
                }
                else
                {
                    Console.WriteLine("DOUBLE DOWN!");
                    Action = "DD";
                }
            }
            else if (handTotal >= 12 && handTotal <= 16)
            {
                if (dealerUpcard.Rank >= Ranks.TWO && dealerUpcard.Rank <= Ranks.SIX)
                {
                    Console.WriteLine("stand");
                }
                else
                {
                    Console.WriteLine("HIT!");
                    Action = "hit";
                }
            }
            else
            {
                Console.WriteLine("stand :)");
            }
            
            return Action;
        }
    }
}
