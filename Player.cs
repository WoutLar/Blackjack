using System;

namespace Blackjack
{
    public class Player
    {
        public Hand Hand { get; }
        public List<Hand> Hands { get; private set; }
        public int BetAmount { get; private set; } = 0;
        
        public int Score { get; private set; } = 0;

        public Player()
        {
            Hand = new Hand();
            Hands = new List<Hand>();
            Hands.Add(Hand);
            
        }

        public virtual void Hit(Deck deck)
        {
            Card drawnCard = deck.Draw();
            Hand.AddCard(drawnCard);
            Console.WriteLine("you drew : " + drawnCard);
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

        public void Split()
        {
            if (Hands == null)
            {
                Console.WriteLine("Cannot split hand.");
                return;
            }

            Hand splitHand = Hand.Split();
            if (splitHand != null)
            {
                Hands.Add(splitHand);
                Console.WriteLine("splitting hand sucesfully");
                Console.WriteLine("Player's hands:");
                foreach (var hand in Hands)
                {
                    hand.Display();
                }
            }
            else
            {
                Console.WriteLine("Cannot split hand.");
            }
        }
        
        public string PlayBasicStrategy(Card dealerUpcard, Deck deck) // logica met hulp van chatgpt gemaakt om te kijken wat de hoogste win percentage is 
        {
            int handTotal = Hand.TotalCardValue;
            string Action = "stand";
            if (Hand.CanSplit())
            {
                Console.WriteLine("SPLIT!");
                Action = "split";
            }
            else if (handTotal <= 8)
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
