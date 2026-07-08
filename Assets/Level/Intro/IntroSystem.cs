using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IntroSystem : MonoBehaviour
{
    //Button startButton.addlistener(Start())
    //Button configButton.addlistener(Config())
    //Button quitButton.addlistener(Quit())
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Button startButton = root.Q<Button>("Start");
        Button QuitButton = root.Q<Button>("Quit");
        Button LayoutButton = root.Q<Button>("Layout");
        startButton.clicked += StartGame;
        QuitButton.clicked += QuitGame;
        LayoutButton.clicked += LayoutConfig;
    }

    private void LayoutConfig()
    {
        LayoutControll.InitLayoutControllToast();
    }

    private void QuitGame()
    {
        //ExitGame Toast
        QuitConfirmToast.InitQuitConfirmToast();
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync("Site");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
