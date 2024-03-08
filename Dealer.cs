
    namespace Blackjack
    {
        public class Dealer : PlayerOptions
        {
            protected override void UpdateScore()
            {
                Score = 0;
                foreach (Card card in Hand)
                {
                    Score += (int)card.Rank;
                }
            }
        }

    }