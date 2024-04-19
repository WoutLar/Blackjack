using System;

public enum Suits
{
    HEARTS,
    DIAMONDS,
    CLUBS,
    SPADES
}

public enum Ranks
{
    ACE = 11,
    TWO = 2,
    THREE,
    FOUR,
    FIVE,
    SIX,
    SEVEN,
    EIGHT,
    NINE,
    TEN,
    JACK = 10,
    QUEEN = 10,
    KING = 10
}

public class Card
{
    public Suits Suit { get; }
    public Ranks Rank { get; }

    public Card(Suits suit, Ranks rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}