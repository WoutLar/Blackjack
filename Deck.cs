using System;

public class Deck
{
    private Pile drawPile = new Pile();
    private Pile discardPile = new Pile();

    public Pile DrawPile => drawPile;
    public Pile DiscardPile => discardPile;

    public Deck()
    {
        InitializeDeck();
        drawPile.Shuffle();
    }

    private void InitializeDeck()
    {
        foreach (Suits suit in Enum.GetValues(typeof(Suits)))
        {
            foreach (Ranks rank in Enum.GetValues(typeof(Ranks)))
            {
                drawPile.Add(new Card(suit, rank));
            }
        }
    }

    public Card Draw()
    {
        if (drawPile.IsEmpty())
        {
            while (!discardPile.IsEmpty())
            {
                drawPile.Add(discardPile.RemoveTop());
            }
            drawPile.Shuffle();
        }

        return drawPile.RemoveTop();
    }

    public void Discard(Card card)
    {
        discardPile.Add(card);
    }
    
    public void ShuffleDrawPile()
    {
        drawPile.Shuffle();
    }
}