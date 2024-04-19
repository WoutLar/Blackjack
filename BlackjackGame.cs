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
        private static List<Player> players = new List<Player>();
        public int betAmount;
        public List<int> betArray = new List<int>(players.Count);

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
            int i = 0;
            foreach (Player player in players)
            {
                betAmount = random.Next(2, 21) * 10;
                betArray.Add(betAmount);
                player.PlaceBet(betAmount);
                Console.WriteLine($"Player {players.IndexOf(player) + 1} placed a bet of {betAmount} chips.");
                i++;
            }

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
                int currentHandIndex = 0;
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s turn:");
                bool stand = false;
                while (!player.Hand.IsBusted() && !stand)
                {
                    Console.WriteLine($"Player {players.IndexOf(player) + 1} is thinking:");
                    ReRandomizer();
                    Thread.Sleep(WaitRandomTime);
                    currentHandIndex = 0;
                    string playerAction = player.PlayBasicStrategy(dealer.Hand.GetUpCard(), deck, currentHandIndex, betAmount);

                    if (playerAction == "hit")
                    {
                        Console.WriteLine($"1 Grab a card for Player {players.IndexOf(player) + 1}");
                        Console.ReadLine();
                        player.Hit(deck, currentHandIndex);
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
                        player.DoubleDown(deck, currentHandIndex, betAmount, player.Chips);
                        Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand:");
                        player.Hand.Display();
                        player.Stand();
                        stand = true;
                    }
                    else if (playerAction == "split")
                    {
                        Console.Write("player Splits! playing 1st hand");
                        Console.Write($"grab card for {players.IndexOf(player) + 1}");
                        Console.ReadLine();
                        player.Split(betAmount);
                        foreach (var hand in player.Hands)
                        {
                            stand = false;
                            while (!stand && !player.Hands[currentHandIndex].IsBusted())
                            {
                                Console.WriteLine($"Player {players.IndexOf(player) + 1} is thinking:");
                                ReRandomizer();
                                Thread.Sleep(WaitRandomTime);
                                string handAction =
                                    player.PlayBasicStrategy(dealer.Hand.GetUpCard(), deck, currentHandIndex, betAmount);

                                if (handAction == "hit")
                                {
                                    Console.WriteLine($"Grab a card for Player {players.IndexOf(player) + 1}" +
                                                      "playing with hand:" + currentHandIndex);
                                    Console.ReadLine();
                                    player.Hit(deck, currentHandIndex);
                                    Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand " +
                                                      currentHandIndex + ":");
                                    hand.Display();
                                }
                                else if (handAction == "stand")
                                {
                                    player.Stand();
                                    stand = true;
                                    Console.WriteLine("no dealer input needed, play on");
                                    Thread.Sleep(1000);
                                }
                                else if (handAction == "DD")
                                {
                                    Console.WriteLine($"1 Grab a card for Player {players.IndexOf(player) + 1}" +
                                                      "playing with hand:" + currentHandIndex);
                                    Console.ReadLine();
                                    player.DoubleDown(deck, currentHandIndex, betAmount, player.Chips);
                                    Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand " +
                                                      currentHandIndex + " : ");
                                    player.Hand.Display();
                                    player.Stand();
                                    stand = true;
                                }
                                else if (playerAction == "split")
                                {
                                    Console.Write("player Splits! playing 1st hand");
                                    Console.Write($"grab card for {players.IndexOf(player) + 1}");
                                    Console.ReadLine();
                                    player.Split(betAmount);
                                }
                            }

                            currentHandIndex++;
                        }
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
    int i=0;
    foreach (Player player in players)
    {
        int currentHandIndex = 0;
        foreach (var hand in player.Hands)
        {
            int chipsBeforeBet = player.Chips;
            
            Console.WriteLine($"Player {players.IndexOf(player) + 1} had {chipsBeforeBet} chips before the bet.");
            Console.WriteLine($"Player {players.IndexOf(player) + 1} betted {betArray[i]} chips.");
            if (player.Hands[currentHandIndex].IsBusted())
            {
                results[player] = GameResult.Loss;
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand {currentHandIndex} busts. They lose.");
                player.Chips -= betArray[i];
            }
            else if (dealer.Hand.IsBusted() ||
                     player.Hands[currentHandIndex].TotalCardValue > dealer.Hand.TotalCardValue)
            {
                results[player] = GameResult.Win;
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand {currentHandIndex} wins.");
                player.Chips += betArray[i]*2;
            }
            else if (player.Hands[currentHandIndex].TotalCardValue < dealer.Hand.TotalCardValue)
            {
                results[player] = GameResult.Loss;
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand {currentHandIndex} loses.");
                player.Chips -= betArray[i];
            }
            else
            {
                results[player] = GameResult.Push;
                Console.WriteLine($"Player {players.IndexOf(player) + 1}'s hand {currentHandIndex} pushes.");
                player.Chips += betArray[i];
            }

            int chipsAfterBet = player.Chips;
            
            Console.WriteLine($"Player {players.IndexOf(player) + 1} has {chipsAfterBet} chips after the bet.");
            
            currentHandIndex++;
        }
        i++;
    }
}

        private int ChooseNumberOfPlayers()
        {
            int numPlayers;
            do //1ste keer dat ik deze statement gebruik dacht dat het handig zou zijn.
            {
                Console.WriteLine("How many players do you want to play with (1-4)?");
            } while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 1 || numPlayers > 4);

            return numPlayers;
        }
    
        public void ReRandomizer()
        {
            WaitRandomTime = random.Next(2000, 7000);
        }
    }
}
