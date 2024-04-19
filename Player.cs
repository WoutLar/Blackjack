using System;

namespace Blackjack
{
    public class Player
    {
        public Hand Hand { get; }
        public List<Hand> Hands { get; set; }

        public int Chips { get; set; } = 400;

        
        public Player()
        {
            Chips = 400;
            Hand = new Hand();
            Hands = new List<Hand>();
            Hands.Add(Hand);
            
        }

        public virtual void Hit(Deck deck, int index)
        {
            Card drawnCard = deck.Draw();
            Hands[index].AddCard(drawnCard);
            Console.WriteLine("you drew : " + drawnCard);
        }

        public virtual void Stand()
        {
            Console.WriteLine("Player stands.");
        }

        public virtual void DoubleDown(Deck deck, int index, int betAmount ,int chips)
        {
            if (betAmount*2 < chips)
            {
                betAmount *= 2;
                Card drawnCard = deck.Draw();
                Hands[index].AddCard(drawnCard);
                Stand();
                betAmount = betAmount * 2;
            }
        }

        public void Split(int betAmount)
        {
            if (Hands == null)
            {
                Console.WriteLine("Cannot split hand.");
                return;
            }

            Hand splitHand = Hand.Split(betAmount, Chips);
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
        
        public void PlaceBet(int amount)
        {
            if (amount <= Chips)
            {
                Chips -= amount;
                Console.WriteLine($"Player placed a bet of {amount} chips.");
            }
            else
            {
                Console.WriteLine("Insufficient chips to place the bet.");
            }
        }
        
        public string PlayBasicStrategy(Card dealerUpcard, Deck deck, int currentHandIndex, int betAmount) // logica met hulp van chatgpt gemaakt om te kijken wat de hoogste win percentage is 
        {
            int handTotal = Hands[currentHandIndex].TotalCardValue;
            string Action = "stand";
            if (Hands[currentHandIndex].CanSplit(betAmount, Chips))
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
