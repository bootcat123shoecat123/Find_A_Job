using MessagePack;
using MessagePack.Resolvers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InterviewFlowController
{
    public List<InterviewQuestionMaster> questions;
    public static InterviewFlowController instance;
    public InterviewCardDeck currentCardDeck;
    public InterviewQuestionDeck currentQuestionDeck;

    // Interview Stats
    public int trust;
    public int like;
    public bool DNF;
    public int selection;
    public int dealCardQuantity;
    public int garde;

    
    public List<InterviewReactionCardMaster> currentDrawCards = new List<InterviewReactionCardMaster>();
    public List<InterviewQuestionMaster> currentDrawQuestions = new List<InterviewQuestionMaster>();

    public InterviewFlowController()
    {
        if (instance != null)
        {
            Debug.LogError("InterviewFlowController instance already exists!");
            return;
        }
        instance = this;
    }
    public void InstantiateInterviewFlow()
    {
        //prepare the interview card deck and interview question deck
        currentCardDeck = InterviewCardDeck.instance;

        currentQuestionDeck = InterviewQuestionDeck.origin;
        //randomly select ten of the interview questions ,put them in a list 

        currentQuestionDeck.questions = DealQuestion(10);


    }
    public void SetInterviewerStats(string change, E_Successfulness successfulness)
    {
        float addition = 0;
        switch (successfulness)
        {
            case E_Successfulness.Good:
                addition = 2.5f;
                break;
            case E_Successfulness.Normal:
                addition = 1.0f;
                break;
            case E_Successfulness.Bad:
                addition = -2.0f;
                break;
            default:
                break;
        }

        // Split the change string into a list of strings
        List<string> changeItem = change.Split(',').ToList<string>();
        int changeValue = 0;
        foreach (string item in changeItem)
        {
            changeValue = int.Parse(item.Substring(1));
            instance.InterviewChangedValue(item[0], changeValue, addition);
        }
        
    }

    public void InterviewChangedValue(char mark, int changeValue, float addition)
    {
        if (changeValue != 0)
        {
            switch (mark)
            {
                case 't':
                    trust += (int)(addition * changeValue);
                    if (trust < 0) GameOver();
                    break;
                case 'l':
                    like += (int)(addition * changeValue);
                    if (like < 0) GameOver();
                    break;
                case 's':
                    selection += (int)(addition * changeValue);
                    if (selection < 0) GameOver();
                    break;
                case 'g':
                    garde += (int)(addition * changeValue);
                    if (garde < 0) GameOver();
                    break;
                default:
                    Debug.LogWarning("changeItem is no match value");
                    break;
            }
        }
    }

    private void GameOver()
    {

        throw new System.NotImplementedException();
    }


    public List<InterviewReactionCardMaster> DealCard(int cardQuantity)
    {
        //clear the current draw cards list
        currentDrawCards.Clear();
        for (int i = 0; i < cardQuantity; i++)
        {
            if (currentCardDeck.cards.Count < 1) break;
            //deal randomly card to the player 
            int randomIndex = Random.Range(0, currentCardDeck.cards.Count);
            InterviewReactionCardMaster currentCard = currentCardDeck.cards[randomIndex];
            //remove the card from the deck 
            currentCardDeck.cards.RemoveAt(randomIndex);
            currentDrawCards.Add(currentCard);
            
        }
        return currentDrawCards;
    }
    public List<InterviewQuestionMaster> DealQuestion(int questionQuantity)
    {
        //clear the current draw question
        currentDrawQuestions.Clear();
        for (int i = 0; i < questionQuantity; i++)
        {

            if (currentQuestionDeck.questions.Count < 1) break;
            //deal the randomly question to the player 
            int randomIndex = Random.Range(0, currentQuestionDeck.questions.Count);
            InterviewQuestionMaster currentQuestion = currentQuestionDeck.questions[randomIndex];
            //remove the question from the deck 
            currentQuestionDeck.questions.RemoveAt(randomIndex);
            currentDrawQuestions.Add(currentQuestion);
            
        }
        return currentDrawQuestions;
    }
    public void JudgeReaction(InterviewReactionCardMaster card)
    {
        SetInterviewerStats(card.Card_Effect, JudgeReactionSuccessfulness(card));
        

    }

    public E_Successfulness JudgeReactionSuccessfulness(InterviewReactionCardMaster card)
    {

        //judge the reaction card's tag with the question's tag,count good and bad tag match
        int good = 0;
        int bad = 0;
        card.Card_Tag.ForEach(tag =>
        {
            currentDrawQuestions.ForEach(question =>
            {
                if (question.Question_Good.Contains(tag))
                {
                    good++;
                }
                else if (question.Question_Bad.Contains(tag))
                {
                    bad++;
                }
                else
                {
                }
            });
        });

        //Max(Good,Bad),if no tag match or equal,return Normal
        if (good>bad)
        {
            return E_Successfulness.Good;
        }
        else if (bad > good)
        {
            return E_Successfulness.Bad;
        }
        else
        {
            return E_Successfulness.Normal;
        }
    }

    public InterviewerMaster ReadInterviewer()
    {
        var messagePackResolvers = CompositeResolver.Create(
           MasterMemory.MasterMemoryResolver.Instance,
           StandardResolver.Instance
       );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var path = "Binary/InterviewerMasterData";
        var asset = Resources.Load<TextAsset>(path);
        var binary = asset.bytes;

        var memoryDatabase = new MasterMemory.MemoryDatabase(binary);

        // Get the interviewer ID from PlayerPrefs, defaulting to "Interviewer_001" if not set
        string interviewerID = PlayerPrefs.GetString("Interviewer")!=null? PlayerPrefs.GetString("Interviewer"): "Interviewer_001";
        return memoryDatabase.InterviewerMasterTable.FindByInterviewer_ID(interviewerID);
    }



    internal InterviewQuestionMaster ReadQuestion(string qID)
    {
        var messagePackResolvers = CompositeResolver.Create(
           MasterMemory.MasterMemoryResolver.Instance,
           StandardResolver.Instance
       );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var path = "Binary/InterviewQuestionMasterData";
        var asset = Resources.Load<TextAsset>(path);
        var binary = asset.bytes;

        var memoryDatabase = new MasterMemory.MemoryDatabase(binary);
        return memoryDatabase.InterviewQuestionMasterTable.FindByQuestion_ID(qID);
    }
}
