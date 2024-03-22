// Dealer.cs
using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Dealer : Player
    {
        public override void Stand()
        {
            Console.WriteLine("Dealer stands.");
        }
        
        public void DealerChoice(Deck deck)
        {
            while (!Hand.IsBusted())    
            {
                Console.WriteLine("Dealer is thinking...");
                Thread.Sleep(100);
                Console.WriteLine("What does the dealer want to do?");
                Console.WriteLine("1 = Hit");
                Console.WriteLine("2 = Stand");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Hit(deck);
                        Hand.Display();
                        break;
                    case "2":
                        Stand();
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }

            if (Hand.IsBusted())
            {
                Console.WriteLine("Dealer Busted"); 
            }
        }

    }
}