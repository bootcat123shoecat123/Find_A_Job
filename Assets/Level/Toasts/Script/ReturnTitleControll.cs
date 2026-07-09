using SupSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ReturnTitleControll : ToastBase
{
    Button rejectButton, acceptButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        
    }
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        rejectButton = root.Q<Button>("B_Reject");
        acceptButton = root.Q<Button>("B_Accept");
        if (rejectButton != null)
            rejectButton.clicked += BackPrev;

        if (acceptButton != null)
            acceptButton.clicked += ReturnTitle;
    }

    private void OnDisable()
    {
        if (rejectButton != null)
            rejectButton.clicked -= BackPrev;
        if (acceptButton != null)
            acceptButton.clicked -= ReturnTitle;
    }

    public static void InitReturnTitleToast()
    {
        CreateToast(Resources.Load("Prefabs/Toast/ReturnTitleAnnouncePrefab"));
    }
    private void ReturnTitle()
    {
        SoundController.Instance.PlayAudio("Accept", SoundController.SoundChannel.SE);
        SceneManager.LoadScene(0);
    }

    private void BackPrev()
    {

        SoundController.Instance.PlayAudio("Reject", SoundController.SoundChannel.SE);
        if (rejectButton != null) { 
            rejectButton.clicked -= BackPrev;
        }
        DestroyToast();
    }

    // Update is called once per frame
    
}
