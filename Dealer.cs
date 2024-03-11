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
        
    }
}