namespace Blackjack
{
    class Program
    {
        public static string userInput;
        public static string[] cardSet;
        public static bool debug;
        static void Main(string[] args)
        {
            cardSet = CardDeck.CreateCardSet();

            if (userInput == "debugOnn")
            {
                debug = true;
                
            }
            
            if (debug)
            {
                foreach (string card in cardSet)
                {
                    Console.WriteLine(card);
                }
                BlackjackGame.StartGame();
            }
        }
    }
}