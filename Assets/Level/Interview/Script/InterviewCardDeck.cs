using System.Collections.Generic;

public class InterviewCardDeck
{
    public static InterviewCardDeck instance { get; internal set; }
    public List<InterviewReactionCardMaster> cards;
    private List<string> sampleCards= new List<string> { "Card_001", "Card_002", "Card_003", "Card_003", "Card_004", "Card_005", "Card_006", "Card_006", "Card_006", "Card_006", "Card_006", "Card_007","Card_007", "Card_008", "Card_008", "Card_008",  "Card_008", "Card_009","Card_009","Card_009","Card_009", "Card_010" , "Card_010" , "Card_010" , "Card_010" , "Card_011", "Card_012", "Card_013" };
    void InitInterviewCardDeck()
    {
        cards = new List<InterviewReactionCardMaster>();
        if(instance == null)
        {
            instance = this;
        }
        //FakeDeck
        sampleCards.ForEach(card => { 
                    
        });
    }
}