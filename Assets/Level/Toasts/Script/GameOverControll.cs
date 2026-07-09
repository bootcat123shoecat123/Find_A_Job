using SupSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverControll : ToastBase
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
            rejectButton.clicked += TryAgain;

        if (acceptButton != null)
            acceptButton.clicked += ReturnSite;
    }

    private void OnDisable()
    {
        if (rejectButton != null)
            rejectButton.clicked -= TryAgain;
        if (acceptButton != null)
            acceptButton.clicked -= ReturnSite;
    }

    public static void InitReturnTitleToast()
    {
        CreateToast(Resources.Load("Prefabs/Toast/ReturnTitleAnnouncePrefab"));
    }
    private void ReturnSite()
    {

        SoundController.Instance.PlayAudio("Accept", SoundController.SoundChannel.SE);
        SceneManager.LoadScene(1);
    }

    private void TryAgain()
    {
        SoundController.Instance.PlayAudio("Reject", SoundController.SoundChannel.SE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        DestroyToast();
    }

    // Update is called once per frame
    
}
