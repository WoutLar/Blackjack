
    namespace Blackjack
    {
        class BlackjackGame
        {
            private static int cardsPlayed;
            public static void StartGame()
            {
                Console.WriteLine("welcome to blackjack");
                Console.WriteLine("start?");
                while (Program.userInput == null)
                {
                    Program.userInput = Console.ReadLine();
                    if (Program.userInput == "y")
                    {
                        BlackJackInitiation();
                    }
                    else
                    {
                        Program.userInput = null;
                    }
                }
            }
            private static string Rank;
            private static string Suit;
            private static int playerIndex = 0;
            private static int dealerIndex = 0;
            private static int playerCardTotal = 0;
            private static int dealerCardTotal = 0;
            private static string[] player1Cards = new string[9];
            private static string[] dealerCards = new string[9];

            public static void BlackJackInitiation()
            {
                
                Console.WriteLine("Player 1 cards =");
                Rank =CardDeck.GetRank(Program.cardSet[cardsPlayed]);
                Suit = CardDeck.GetSuit(Program.cardSet[cardsPlayed]);
                CardDeck.PrintCard(Rank, Suit);
                player1Cards[playerIndex] = Program.cardSet[cardsPlayed];
                if (Program.debug)
                {
                    Console.WriteLine(player1Cards[playerIndex]);
                }
                
                cardsPlayed++;
                
                Thread.Sleep(1000);
                
                Console.WriteLine("Dealer Cards cards =");
                Rank =CardDeck.GetRank(Program.cardSet[cardsPlayed]);
                Suit = CardDeck.GetSuit(Program.cardSet[cardsPlayed]);
                CardDeck.PrintCard(Rank, Suit);
                dealerCards[dealerIndex] = Program.cardSet[cardsPlayed];
                if (Program.debug)
                {
                    Console.WriteLine(dealerCards[dealerIndex]);
                }
                
                cardsPlayed++;
                
                Thread.Sleep(100);
                if (Rank == "A")
                {
                    Console.WriteLine("Insurance bet can be placed: (y/n)");
                    Program.userInput = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Insurance bet is not needed: play on.");
                }
                
                Thread.Sleep(1000);

                BlackJackASCII();
            }

            public static void BlackJackASCII()
            {
                Console.WriteLine("Player 1 cards =");
                Rank =CardDeck.GetRank(player1Cards[playerIndex]);
                Suit = CardDeck.GetSuit(player1Cards[playerIndex]);
                CardDeck.PrintCard(Rank, Suit);
                CardDeck.PrintDummyCard();
                
                playerIndex++;
                player1Cards[playerIndex] = Program.cardSet[cardsPlayed];
                cardsPlayed++;
                
                Thread.Sleep(1000);
                
                DealerHand();
                
                Thread.Sleep(1000);
                
                BlackJackGameLoop();
            }

            public static void BlackJackGameLoop()
            {
                Console.WriteLine("Player 1 cards =");
                playerIndex = 0;
                foreach (string card in player1Cards)
                {
                    try
                    {
                        Rank = CardDeck.GetRank(card);
                        Suit = CardDeck.GetSuit(card);
                        CardDeck.PrintCard(Rank, Suit);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                Console.WriteLine(calcTotal(player1Cards));

                bool choice = false;
                if (calcTotal(player1Cards) != 21)
                {
                    while (choice == false)
                    {
                        Console.WriteLine("Hit (h) - Stand (s) - dubble down (dd) - Dealers hand (dh)?");
                        Program.userInput = Console.ReadLine();
                        if (Program.userInput == "h")
                        {
                            hit(player1Cards);
                            choice = true;
                        }

                        if (Program.userInput == "s")
                        {
                            choice = true;
                        }
                        if (Program.userInput == "dh")
                        {
                            Console.WriteLine("Dealers hand:");
                            DealerHand();
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Blackjack!");
                }
            }

            public static void DealerHand()
            {
                Console.WriteLine("Dealer cards =");
                Rank =CardDeck.GetRank(dealerCards[dealerIndex]);
                Suit = CardDeck.GetSuit(dealerCards[dealerIndex]);
                CardDeck.PrintCard(Rank, Suit);
                CardDeck.PrintDummyCard();
                
                dealerIndex++;
                dealerCards[dealerIndex] = Program.cardSet[cardsPlayed];
                cardsPlayed++;
            }

            public static void hit(string[] cards)
            {
                if (calcTotal(cards) < 21)
                {
                    
                }   
            }
            
            public static int calcTotal(string[] cards)
            {
                int ind = 0;
                int total = 0;
                foreach (string card in cards)
                {
                    try
                    {
                        Rank = CardDeck.GetRank(cards[ind]);
                        if (Rank == "J" || Rank == "Q" || Rank == "K")
                        {
                            total += 10;
                        }
                        else if (Rank == "A")
                        {
                            if (total + 11 <= 21)
                            {
                                total += 11;
                            }
                            else if (total + 1 <= 21)
                            {
                                total += 1;
                            }
                            
                        }
                        else if (int.TryParse(Rank, out int numericRank))
                        {
                            total += numericRank;
                        }

                        ind++;

                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }

                return total;
            }
        }
    }