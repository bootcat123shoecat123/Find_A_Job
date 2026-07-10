using MessagePack;
using MessagePack.Resolvers;
using SupSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InterviewResponeser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    InterviewFlowController interviewFlowController;
    VisualElement cardDeck;
    int turnIndex = 0;
    // use for waiting for the player to choose a card
    private TaskCompletionSource<bool> chooseCardTcs;
    // the card that the player has selected
    private InterviewReactionCardMaster selectedReactionCard;

    void Start()
    {

    }
    private void OnEnable()
    {

        interviewFlowController = new InterviewFlowController();
        interviewFlowController.InstantiateInterviewFlow();
        var root = GetComponent<UIDocument>().rootVisualElement;
        // CardDeck which is for put the card buttons
        cardDeck = root.Q<VisualElement>("CardDeck");

        SetInterviewer();
        
        Task_StartNewTurn();
    }

    private void SetInterviewer()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Image>("IMG_Interviewer").sprite = Resources.Load<Sprite>(interviewFlowController.currrentInterviewer.Interviewer_Path);
    }

    public async void Task_StartNewTurn()
    {
        //future:Custom Question Quantity
        if (turnIndex >= 5) { interviewFlowController.Win(); }
        // show the question to the player
        ShowQuestionToPlayer(interviewFlowController.currentQuestionDeck.questions[turnIndex]);
        // wait for the player to watch the question and click the button to continue
        // deal three cards to the player
        interviewFlowController.DealCard(3);
        ShowCardToPlayer(interviewFlowController.currentDrawCards);

        //future: wait for the draw animation to finish before allowing the player to choose a card
        //await WaitForDrawAnimationOver();



        // player has selected a card, now judge the reaction based on the selected card
        

        //await WaitForPlayerToClickAnyting();

    }

    

    private void ShowCardToPlayer(List<InterviewReactionCardMaster> currentDrawCards)
    {

        // Clear Deck
        cardDeck.Clear();

        // Card Generate
        GenerateResponeCards();

        // check cardDeck
        if (cardDeck == null)
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            cardDeck = root.Q<VisualElement>("CardDeck");
            if (cardDeck == null)
            {
                Debug.LogError("CardDeck VisualElement not found in UIDocument.");
                return;
            }
        }
    }
        

    private async Task WaitForPlayerToChooseCard()
    {
        // if which select,return
        if (selectedReactionCard != null)
        {
            return;
        }

        chooseCardTcs = new TaskCompletionSource<bool>();
        // 等待玩家點擊卡片
        await chooseCardTcs.Task;
        // 清理
        chooseCardTcs = null;
    }

    private async Task WaitForPlayerToClickAnyting()
    {
        // 嘗試找到一個名為 "ContinueButton" 的按鈕，並等待被點擊
        var root = GetComponent<UIDocument>().rootVisualElement;
        var continueBtn = root.Q<Button>("B_TalkBubble");

        var tcs = new TaskCompletionSource<bool>();
        void OnClick()
        {
            tcs.TrySetResult(true);
            continueBtn.clicked -= OnClick;
        }

        continueBtn.clicked += OnClick;
        await tcs.Task;
    }

    private void ShowQuestionToPlayer(InterviewQuestionMaster interviewQuestionMaster)
    {
        SoundController.Instance.PlayAudio("Question", SoundController.SoundChannel.SE);
        // put in TalkBubble
        var root = GetComponent<UIDocument>().rootVisualElement;
        var talkBubble = root.Q<Button>("B_TalkBubble");
        if (talkBubble != null && interviewQuestionMaster != null)
        {
            talkBubble.text = interviewQuestionMaster.Question_Sentence;
            talkBubble.enabledSelf = true;
        }
    }
    //Todo:
    void GenerateResponeCards()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("CardDeck");

        interviewFlowController.currentDrawCards.ForEach (card => {
        
            var cardPrefab = Resources.Load<VisualTreeAsset>("Prefabs/Interview/UX_ResponeCard").Instantiate();

            Button cardRoot = cardPrefab.Q<Button>("B_CardPanel");
            cardRoot.Q<Label>("T_CardName").text = card.Card_Name;
            cardRoot.Q<Label>("T_CardSkill").text = card.Card_Effect.Replace(',','\n');

            //future:Let Card Have Image Self
            //cardRoot.Q<Image>("IMG_JobImage").sprite = Resources.Load<Sprite>(job.Job_ImagePath);
            

            cardRoot.clicked += () =>
            {
                PlayTheCard(card);
            };
            root.Add(cardPrefab);
            
        });

    }

    private void PlayTheCard(InterviewReactionCardMaster card)
    {
        SoundController.Instance.PlayAudio("ValueUP", SoundController.SoundChannel.SE);
        interviewFlowController. JudgeReaction(card);
        turnIndex++;
        Task_StartNewTurn();
    }
    


}
