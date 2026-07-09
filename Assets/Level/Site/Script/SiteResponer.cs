using MessagePack;
using MessagePack.Resolvers;
using SupSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SiteResponer : MonoBehaviour
{
    Button jobListButton, partTimeButton,titleButton,interviewListButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
    }
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        jobListButton = root.Q<Button>("B_JobList");
        partTimeButton = root.Q<Button>("B_PartTime");
        titleButton = root.Q<Button>("B_Title");
        interviewListButton = root.Q<Button>("B_InterviewList");
        //todo: other pages are not implemented yet, so the buttons are disabled for now
        /*
        if (jobListButton != null)
            jobListButton.clicked += () => SwichPage(1);
        if (partTimeButton != null)
            partTimeButton.clicked += () => SwichPage(2);
        
        if (interviewListButton != null)
            interviewListButton.clicked += () => SwichPage(4);
        */
        if (titleButton != null)
            titleButton.clicked += ReturnTitle;
        GenerateJobCards();
    }
    void ReturnTitle() { 
        ReturnTitleControll.InitReturnTitleToast();
    }

    void SwichPage(int page)
    {
        //Enable the page and disable the other pages
    }
    void GenerateJobCards()
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

        for (int index = 1; index <= 4; index++)
        {
            JobMaster job = memoryDatabase.JobMasterTable.FindByJob_ID("Job_" + $"{index:000}");
            var cardPrefab = Instantiate( Resources.Load<VisualTreeAsset>("Prefabs/Site/UX_Jobprefab"));
            
            VisualElement cardRoot=cardPrefab.Instantiate();
            cardRoot.tabIndex = index;
            cardRoot.Q<Label>("T_JobCompany").text = job.Job_Company;
            cardRoot.Q<Label>("T_JobWork").text = job.Job_Work;
            cardRoot.Q<Label>("T_JobLoc").text = job.Job_Loc;
            cardRoot.Q<Label>("T_JobTel").text = job.Job_Tel;
            cardRoot.Q<Image>("IMG_JobImage").sprite = Resources.Load<Sprite>(job.Job_ImagePath);
            cardRoot.Q<Button>("B_Like").clicked += () =>
            {
                SoundController.Instance.PlayAudio("Start", SoundController.SoundChannel.SE);
                PlayerPrefs.SetString("Interviewer", job.Job_Interviewer);
                SceneManager.LoadScene("Interview");
            };
            root.Add(cardRoot);
        }
        
    }
}

