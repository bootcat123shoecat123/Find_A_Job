using SupSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class QuitConfirmControll : ToastBase
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
            acceptButton.clicked += QuitGame;
    }

    private void OnDisable()
    {
        if (rejectButton != null)
            rejectButton.clicked -= BackPrev;
        if (acceptButton != null)
            acceptButton.clicked -= QuitGame;
    }

    public static void InitQuitConfirmToast()
    {
        CreateToast(Resources.Load("Prefabs/Toast/QuitAnnouncePrefab"));
    }
    private void QuitGame()
    {
        Application.Quit();
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
