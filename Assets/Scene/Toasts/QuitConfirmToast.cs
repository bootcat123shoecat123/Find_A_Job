using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class QuitConfirmToast : ToastBase
{
    Button rejectButton, acceptButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        var root = GetComponent<UIDocument>().rootVisualElement;
        rejectButton = root.Q<Button>("Reject");
        rejectButton.clicked += BackPrev;
        acceptButton = root.Q<Button>("Accept");
        acceptButton.clicked += QuitGame;
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
        if (rejectButton != null) { 
            rejectButton.clicked -= BackPrev;
        }
        DestroyToast();
    }

    // Update is called once per frame
    
}
