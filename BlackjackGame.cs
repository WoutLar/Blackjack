﻿using System;
using System.Collections.Generic;

namespace Blackjack
{
    class BlackjackGame
    {
        private static Random random = new Random();
        private int WaitRandomTime = 0;
        private Deck deck;
        private Dealer dealer;
        private List<Player> players = new List<Player>();

        public BlackjackGame()
        {
            deck = new Deck();
            dealer = new Dealer();
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
                            deck.ShuffleDrawPile();
                            Console.WriteLine("The deck has been shuffled.");
                        }
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
            }
            Thread.Sleep(8000);
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
            }
            
            PlayerTurns();
        }

        
        private void PlayerTurns()
        {
            foreach (Player player in players)
            {
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s turn:");
                bool stand = false;
                while (!player.Hand.IsBusted() || stand == false)
                {
                    Console.WriteLine($"Player {players.IndexOf(player) + 1} is thinking:");
                    Random random = new Random();
                    int waitTime = random.Next(2000, 9000); // 2-9 sec delay
                    Thread.Sleep(waitTime);
                     string playerAction  = player.PlayBasicStrategy(dealer.Hand.GetUpCard(), deck);
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
                     }else if (playerAction == "DD")
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
