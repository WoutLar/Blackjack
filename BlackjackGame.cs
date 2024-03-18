using System;
using System.Collections.Generic;

namespace Blackjack
{
    class BlackjackGame
    {
        
        private static Random random = new Random();
        private int WaitRandomTime = 0;
        private Deck deck;
        private Pile pile;
        private Dealer dealer;
        private List<Player> players = new List<Player>();

        public BlackjackGame()
        {
            dealer = new Dealer();
        }

        public void ResetHands()
        {
            foreach (Player player in players)
            {
                player.Hand.Clear();
            }
            dealer.Hand.Clear();
        }

        
        public void SetupGame()
        {
            bool setupComplete = false;

            while (!setupComplete)
            {
                Console.WriteLine("Let's start the Game. Here are some options:");
                Console.WriteLine("1. Grab a new deck");
                Console.WriteLine("2. Shuffle the deck");
                Console.WriteLine("3. Choose number of players");
                Console.WriteLine("4. Start game");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        if (deck != null)
                        {
                            deck.Delete();
                            deck = null;
                        }
                        
                        deck = new Deck();
                        Console.WriteLine("A new deck has been grabbed.");
                        break;


                    case "2":
                        if (deck == null)
                        {
                            Console.WriteLine("You need to grab a new deck first.");
                        }
                        else
                        {
                            pile = new Pile();
                            pile.Shuffle();
                            Console.WriteLine("The deck has been shuffled.");
                        }
                        break;

                        break;
                    case "3":
                        if (deck == null)
                        {
                            Console.WriteLine("You need to grab a new deck first.");
                        }
                        else
                        {
                            int numPlayers = ChooseNumberOfPlayers();
                            for (int i = 0; i < numPlayers; i++)
                            {
                                players.Add(new Player());
                            }
                            Console.WriteLine($"You have chosen {numPlayers} players.");
                        }
                        break;
                    case "4":
                        if (deck == null)
                        {
                            Console.WriteLine("You need to grab a new deck first.");
                        }
                        else if (players.Count == 0)
                        {
                            Console.WriteLine("You need to choose the number of players first.");
                        }
                        else
                        {
                            setupComplete = true;
                            Console.WriteLine("Game setup complete.");
                            StartGame();
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private void StartGame()
        {
            foreach (Player player in players)
            {
                player.Hand.AddCard(deck.Draw());
            }
            dealer.Hand.AddCard(deck.Draw());
            Console.WriteLine("Dealers cards:");
            dealer.Hand.DealerDisplay();
            foreach (Player player in players)
            {
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand:");
                player.Hand.Display();
                Thread.Sleep(2000);
            }
            Thread.Sleep(4000);
            foreach (Player player in players)
            {
                player.Hand.AddCard(deck.Draw());
            }
            dealer.Hand.AddCard(deck.Draw());
            Console.WriteLine("Dealers cards:");
            dealer.Hand.DealerDisplay();
            
            Card dealerUpCard = dealer.Hand.GetUpCard();
            if (dealerUpCard.Rank == Ranks.ACE)
            {
                Console.WriteLine("Dealer's face-up card is an Ace. You can place an insurance bet.");
                foreach (Player player in players)
                {
                    Console.WriteLine("Placeholder message Insurrance bet not added Yet PLay on");
                }
            }
            foreach (Player player in players)
            {
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand:");
                player.Hand.Display();
                Thread.Sleep(2000);
            }
            
            
        }

        
        public void PlayerTurns()
        {
            foreach (Player player in players)
            {
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s turn:");
                bool stand = false;
                while (!player.Hand.IsBusted() && !stand)
                {
                    Console.WriteLine($"Player {players.IndexOf(player) + 1} is thinking:");
                    ReRandomizer();
                    Thread.Sleep(WaitRandomTime);
                    string playerAction = player.PlayBasicStrategy(dealer.Hand.GetUpCard(), deck); // logica op Player.cs
    
                    if (playerAction == "hit")
                    {
                        Console.WriteLine($"1 Grab a card for Player {players.IndexOf(player) + 1}");
                        Console.ReadLine();
                        player.Hit(deck);
                        Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand:");
                        player.Hand.Display();
                    }
                    else if (playerAction == "stand")
                    {
                        player.Stand();
                        stand = true;
                        Console.WriteLine("no dealer input needed, play on");
                        Thread.Sleep(1000);
                    }
                    else if (playerAction == "DD")
                    {
                        Console.WriteLine($"1 Grab a card for Player {players.IndexOf(player) + 1}");
                        Console.ReadLine();
                        player.DoubleDown(deck);
                        Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand:");
                        player.Hand.Display();
                        player.Stand();
                        stand = true;
                    }
                }
            }
        }

        public void DealerTurn()
        {
            Console.Write("Dealers turn:");
            Console.WriteLine("what will you do?");
            Console.WriteLine("(FOR NOW! nothing matters what you input)");
            Console.ReadLine();
            Console.WriteLine("Dealer's hand:");
            dealer.Hand.DealerDisplay();
            Thread.Sleep(1000);
            Console.WriteLine("Dealer's hand:");
            dealer.Hand.Display();
            Thread.Sleep(1000);
            dealer.DealerChoice(deck);
        }
        
        private enum GameResult // wou eerst boolean gebruiken maar realizeerden dat je ook gelijk kon spelen
        {
            Win,
            Loss,
            Push
        }

        public void CheckRewards()
        {
            Dictionary<Player, GameResult> results = new Dictionary<Player, GameResult>();

            foreach (Player player in players)
            {
                if (player.Hand.IsBusted())
                {
                    results[player] = GameResult.Loss;
                    Console.WriteLine($"Player {players.IndexOf(player)} busts. They lose.");
                }
                else if (dealer.Hand.IsBusted() || player.Hand.Score > dealer.Hand.Score)
                {
                    results[player] = GameResult.Win;
                    Console.WriteLine($"Player {players.IndexOf(player)} wins.");
                }
                else if (player.Hand.Score < dealer.Hand.Score)
                {
                    results[player] = GameResult.Loss;
                    Console.WriteLine($"Player {players.IndexOf(player)} loses.");
                }   
                else
                {
                    results[player] = GameResult.Push;
                    Console.WriteLine($"Player {players.IndexOf(player)} pushes.");
                }
            }
        }

        


        private int ChooseNumberOfPlayers()
        {
            int numPlayers;
            do
            {
                Console.WriteLine("How many players do you want to play with (1-4)?");
            } while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 1 || numPlayers > 4);

            return numPlayers;
        }
    
        public void ReRandomizer()
        {
            WaitRandomTime = random.Next(3000, 9000);
        }
    }
}
