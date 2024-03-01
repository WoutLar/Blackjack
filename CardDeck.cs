namespace Blackjack
{
    class CardDeck
    {
        public static string[] CreateCardSet()
        {
            string[] suits = { "♥", "♦", "♣", "♠" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] cards = new string[suits.Length * ranks.Length];
            int index = 0;
            
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; j++)
                {
                    cards[index] = ranks[j] + " of " + suits[i];
                    index++;
                }
            }
            
            return cards;
        }

        public static void ShuffleCards(string[] cards)
        {
            Random rng = new Random();
            int deckLength = cards.Length;
            while (deckLength > 1)
            {
                deckLength--;
                int max = rng.Next(deckLength + 1);
                string temp = cards[max];
                cards[max] = cards[deckLength];
                cards[deckLength] = temp;
            }
        }

        public static void PrintCard(string rank, string suit)
        {
            Console.WriteLine(" _________");
            Thread.Sleep(100);
            Console.WriteLine($"| {rank}       |");
            Thread.Sleep(100);
            Console.WriteLine($"|    {suit}    |");
            Thread.Sleep(100);
            Console.WriteLine("|         |");
            Thread.Sleep(100);
            Console.WriteLine("|         |");
            Thread.Sleep(100);
            Console.WriteLine($"|    {suit}    |");
            Thread.Sleep(100);
            Console.WriteLine($"|       {rank} |");
            Thread.Sleep(100);
            Console.WriteLine(" --------- ");
            Thread.Sleep(100);
        }
        public static void PrintDummyCard()
        {
            Console.WriteLine(" _________");
            Thread.Sleep(100);
            Console.WriteLine("|░░░░░░░░░|");
            Thread.Sleep(100);
            Console.WriteLine("|░░░░░░░░░|");
            Thread.Sleep(100);
            Console.WriteLine("|░░░░░░░░░|");
            Thread.Sleep(100);
            Console.WriteLine("|░░░░░░░░░|");
            Thread.Sleep(100);
            Console.WriteLine("|░░░░░░░░░|");
            Thread.Sleep(100);
            Console.WriteLine(" --------- ");
            Thread.Sleep(100);
        }


        public static string GetRank(string card)
        {
            return card.Split()[0];
        }

        public static string GetSuit(string card)
        {
            return card.Split()[2];
        }
        

    }
}