using System.Collections.Generic;

public class InterviewCardDeck
{
    public static InterviewCardDeck origin { get; internal set; }
    public List<InterviewReactionCard> cards;
    void InitInterviewCardDeck(InterviewCardDeck interviewCard)
    {
        if(interviewCard == null)
        {
            origin = this;
        }
        cards = new List<InterviewReactionCard>();
    }
}