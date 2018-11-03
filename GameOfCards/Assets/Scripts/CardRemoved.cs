using System;

public class CardRemoved : EventArgs
{
    public int CIndex { get; private set; }
    public CardRemoved(int cardIndex)
    {
        CIndex = cardIndex;
    }
}