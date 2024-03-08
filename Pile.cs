public class Pile
{
    private List<Card> cards = new List<Card>();

    public bool IsEmpty()
    {
        return cards.Count == 0;
    }
    
    public void Add(Card card)
    {
        cards.Add(card);
    }

    public Card RemoveTop()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("Pile is empty.");
        }

        Card topCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return topCard;
    }

    public void Shuffle()
    {
        Random rng = new Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public void DisplayInOrder()
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(cards[i]);
        }
        Console.WriteLine();
    }
}