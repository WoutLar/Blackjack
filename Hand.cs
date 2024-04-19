using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Hand
    {
        private List<Card> cards = new List<Card>();
        public int TotalCardValue { get; private set; } = 0;

        public void AddCard(Card card)
        {
            cards.Add(card);
            UpdateScore();
        }

        public void Display()
        {
            UpdateScore();
            foreach (var card in cards)
            {
                Console.WriteLine("");
                Console.WriteLine("[ - " + card + " - ]");
                Thread.Sleep(200);
            }

            Console.WriteLine("");
            Console.WriteLine($"Total Score: {TotalCardValue}");
            Console.WriteLine("-----------------------");
        }

        public void DealerDisplay()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (i == 1)
                {
                    Console.WriteLine("");
                    Console.WriteLine("[ - " + cards[i] + " - ]");
                    Thread.Sleep(400);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("[ - xxxxxxxxx - ]");
                }
            }

            Console.WriteLine("");
            Console.WriteLine("----------------------");
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

            TotalCardValue = score;
        }

        public bool HasBlackJack()
        {
            return TotalCardValue == 21;
        }


        public bool IsBusted()
        {
            return TotalCardValue > 21;
        }


        public void Clear()
        {
            cards.Clear();
            TotalCardValue = 0;
        }

        public Hand Split(int betAmount, int chips)
        {
            if (CanSplit(betAmount, chips))
            {
                Hand newHand = new Hand();
                newHand.AddCard(cards[1]);
                cards.RemoveAt(1);
                return newHand;
            }
            else
            {
                return null;
            }
        }

        public bool CanSplit(int betAmount, int chips)
        {
            if (cards.Count == 2 && cards[0].Rank == cards[1].Rank && betAmount * 2 <= chips)
            {
                return true;
            }
            else
            {
                return false;
            }
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