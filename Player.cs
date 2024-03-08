
    namespace Blackjack
    {
        public class Player : PlayerOptions
        {
            protected override void UpdateScore()
            {
                Score = 0;
                int numberOfAces = 0;
                foreach (Card card in Hand)
                {
                    if (card.Rank == Ranks.ACE)
                    {
                        numberOfAces++;
                    }
                    Score += (int)card.Rank;
                }
                while (Score > 21 && numberOfAces > 0)
                {
                    Score -= 10;
                    numberOfAces--;
                }
            }
        }

    }