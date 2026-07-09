using MessagePack;
using MessagePack.Resolvers;
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

    // use for waiting for the player to choose a card
    private TaskCompletionSource<bool> chooseCardTcs;
    // the card that the player has selected
    private InterviewReactionCardMaster selectedReactionCard;
    // 新增於類別欄位區
    private TaskCompletionSource<bool> continueButtonTcs;
    void Start()
    {

    }
    private void OnEnable()
    {

        interviewFlowController = new InterviewFlowController();
        var root = GetComponent<UIDocument>().rootVisualElement;
        // CardDeck which is for put the card buttons
        cardDeck = root.Q<VisualElement>("CardDeck");

        InitInterview();
        
        Task_StartNewTurn();
    }


    public async void Task_StartNewTurn()
    {
        // show the question to the player
        interviewFlowController.DealQuestion(1);

        ShowQuestionToPlayer(interviewFlowController.currentDrawQuestions[0]);
        await WaitForPlayerToClickAnyting();
        // wait for the player to watch the question and click the button to continue
        // deal three cards to the player
        interviewFlowController.DealCard(3);
        ShowCardToPlayer(interviewFlowController.currentDrawCards);

        //future: wait for the draw animation to finish before allowing the player to choose a card
        //await WaitForDrawAnimationOver();


        await WaitForPlayerToChooseCard();

        // player has selected a card, now judge the reaction based on the selected card
        if (selectedReactionCard != null)
        {
            interviewFlowController.JudgeReaction(selectedReactionCard);
        }
        else
        {
            Debug.LogWarning("No card was selected before judging.");
        }

        await WaitForPlayerToClickAnyting();

    }


    private void ShowCardToPlayer(List<InterviewReactionCardMaster> currentDrawCards)
    {
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

        // 清除舊的按鈕
        cardDeck.Clear();

        // 為每張卡建立按鈕，點擊時設定 selectedReactionCard 並完成 Task
        foreach (var card in currentDrawCards)
        {
            var btn = new Button(() =>
            {
                // 這個 lambda 會在按鈕被點擊後執行
                Debug.Log($"Card {card.Card_ID} clicked");
                selectedReactionCard = card;
                // 設定 TCS 結果，解除等待（若有設定）
                chooseCardTcs?.TrySetResult(true);
            })
            { text = card.Card_Text };

            cardDeck.Add(btn);
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
        var continueBtn = root.Q<Button>("ContinueButton");
        if (continueBtn == null)
        {
            // 如果找不到，短暫等待以免死等
            await Task.Delay(200);
            return;
        }

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
        // 範例實作：將問題文字放到名為 "QuestionLabel" 的 Label
        var root = GetComponent<UIDocument>().rootVisualElement;
        var qLabel = root.Q<Label>("QuestionLabel");
        if (qLabel != null && interviewQuestionMaster != null)
        {
            qLabel.text = interviewQuestionMaster.Question_Sentence;
        }
    }
    //Todo:
    void GenerateResponeCards()
    {
        
        var messagePackResolvers = CompositeResolver.Create(
           MasterMemory.MasterMemoryResolver.Instance,
           StandardResolver.Instance
       );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        var path = "Binary/JobMasterData";
        var asset = Resources.Load<TextAsset>(path);
        var binary = asset.bytes;

        var memoryDatabase = new MasterMemory.MemoryDatabase(binary);

        VisualElement root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("WorkList");

        interviewFlowController.currentDrawCards.ForEach (card => {
        
            InterviewReactionCardMaster reactionCard = memoryDatabase.InterviewReactionCardMasterTable.FindByCard_ID(card.Card_ID);
            var cardPrefab = Resources.Load<VisualTreeAsset>("Prefabs/Site/UX_ResponeCard");

            Button cardRoot = cardPrefab.Instantiate().Q<Button>("B_CardPanel");
            cardRoot.Q<Label>("T_CardName").text = card.Card_Name;
            cardRoot.Q<Label>("T_CardSkill").text = card.Card_Effect.Replace(',','\n');

            //future:Let Card Have Image Self
            //cardRoot.Q<Image>("IMG_JobImage").sprite = Resources.Load<Sprite>(job.Job_ImagePath);
            

            cardRoot.clicked += () =>
            {
                PlayTheCard();
            };
            root.Add(cardRoot);
            
        });

    }

    private void PlayTheCard()
    {

        throw new NotImplementedException();
    }

    void InitInterview()
    {
        InterviewerMaster interviewer= interviewFlowController.ReadInterviewer();

        Debug.Log(interviewer.Interviewer_Question);
        List<string> QuestionIDList = interviewer.Interviewer_QuestionArray.ToList<string>();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Debug.Log(interviewer);
        root.Q<Image>("IMG_Interviewer").sprite = Resources.Load<Sprite>(interviewer.Interviewer_Path);
        Debug.Log(QuestionIDList);
        QuestionIDList.ForEach(qID =>
        {
            InterviewQuestionMaster question = interviewFlowController.ReadQuestion(qID);
            interviewFlowController.currentQuestionDeck.questions.Add(question);
        });
        Debug.Log( QuestionIDList.Count);
    }

}
