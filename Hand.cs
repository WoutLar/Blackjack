using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Hand
    {
        private List<Card> cards = new List<Card>();
        public int Score { get; private set; } = 0;

        public void AddCard(Card card)
        {
            cards.Add(card);
            UpdateScore();
        }

        public void Display()
        {
            foreach (var card in cards)
            {
                Console.WriteLine("[ - "+card+" - ]");
            }
            Console.WriteLine($"Total Score: {Score}");
        }

        public void DealerDisplay()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (i == 1) 
                {
                    Console.WriteLine("[ - " + cards[i] + " - ]");
                }
                else
                {
                    Console.WriteLine("[ - xxxxxxxxx - ]");
                }
            }
        }

        private void UpdateScore()
        {
            int score = 0;
            int Maxscore = 21;
            int numberOfAces = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Ranks.ACE)
                {
                    numberOfAces++;
                }

                score += (int)card.Rank;
            }
            
            while (score > Maxscore && numberOfAces > 0)
            {
                score -= 10;
                numberOfAces--;
            }

            Score = score;
        }

        public bool HasBlackJack()
        {
            return cards.Count == 2 && Score == 21;
        }

        
        public bool IsBusted()
        {
            return Score > 21;
        }

        
        public void Clear()
        {
            cards.Clear();
            Score = 0;
        }

        
        public Hand Split()
        {
            if (cards.Count != 2 || cards[0].Rank != cards[1].Rank)
            {
                return null;
            }
            Hand newHand = new Hand();
                
            newHand.AddCard(cards[1]);
            cards.RemoveAt(1);

            return newHand;
        }
        
        public Card GetUpCard()
        {
            if (cards.Count >= 2)
            {
                return cards[1];
            }
            else
            {
                return null;
            }
        }


    }
}