using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterviewFlowController
{
    public List<InterviewQuestion> questions;
    public static InterviewFlowController instance;
    public InterviewCardDeck currentCardDeck;
    public InterviewQuestionDeck currentQuestionDeck;

    // Interview Stats
    public int trust;
    public int like;
    public bool DNF;
    public InterviewQuestionDeck OriginQuestionDeck;
    public int selection;
    public int dealCardQuantity;
    public int garde;



    public void InterviewChangedValue(char mark, int changeValue, float addition)
    {
        if (changeValue != 0)
        {
            switch (mark)
            {
                case 't':
                    trust += (int)(addition * changeValue);
                    break;
                case 'l':
                    like += (int)(addition * changeValue);
                    break;
                case 's':
                    selection += (int)(addition * changeValue);
                    break;
                case 'd':
                    dealCardQuantity += (int)(addition * changeValue);
                    break;
                case 'g':
                    garde += (int)(addition * changeValue);
                    break;
                default:
                    Debug.LogWarning("changeItem is no match value");
                    break;
            }
        }
    }
    public InterviewFlowController()
    {
        if(instance != null)
        {
            Debug.LogError("InterviewFlowController instance already exists!");
            return;
        }
        instance = this;
        InstantiateInterviewFlow();
    }
    public void InstantiateInterviewFlow()
    {
        //prepare the interview card deck and interview question deck
        currentCardDeck = InterviewCardDeck.origin;
        currentQuestionDeck = InterviewQuestionDeck.origin;
        //randomly select ten of the interview questions ,put them in a list 
        
        currentQuestionDeck.questions=DealQuestion(10);
        

    }

    public List<InterviewReactionCard> DealCard(int cardQuantity)
    {
        List<InterviewReactionCard> dealCards = new List<InterviewReactionCard>();
        for (int i = 0; i < cardQuantity; i++)
        {
            if (currentCardDeck.cards.Count < 1) break;
            //deal randomly card to the player 
            int randomIndex = Random.Range(0, currentCardDeck.cards.Count);
            InterviewReactionCard currentCard = currentCardDeck.cards[randomIndex];
            //remove the card from the deck 
            currentCardDeck.cards.RemoveAt(randomIndex);
            dealCards.Add(currentCard);
            
        }
        return dealCards;
    }
    public List<InterviewQuestion> DealQuestion(int questionQuantity)
    {
        List<InterviewQuestion> dealQuestion = new List<InterviewQuestion>();
        for (int i = 0; i < questionQuantity; i++)
        {

            if (currentQuestionDeck.questions.Count < 1) break;
            //deal the randomly question to the player 
            int randomIndex = Random.Range(0, currentQuestionDeck.questions.Count);
            InterviewQuestion currentQuestion = currentQuestionDeck.questions[randomIndex];
            //remove the question from the deck 
            currentQuestionDeck.questions.RemoveAt(randomIndex);
            dealQuestion.Add(currentQuestion);
            
        }
        return dealQuestion;
    }
    
}
