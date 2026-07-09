using SupSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IntroResponer : MonoBehaviour
{
    Button startButton, quitButton, layoutButton, volumeButton;
    // Start is called before the first frame update
    void Start()
    {
        
        SoundController.Instance.PlayAudio("TitleBGM",SoundController.SoundChannel.BGM,true);
    }
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("B_Start");
        quitButton = root.Q<Button>("B_Quit");
        layoutButton = root.Q<Button>("B_Layout");
        volumeButton = root.Q<Button>("B_Volume");
        startButton.clicked += StartGame;
        quitButton.clicked += QuitGame;
        layoutButton.clicked += LayoutConfig;
        volumeButton.clicked += VolumeConfig;
    }
    private void OnDisable()
    {
        startButton.clicked -= StartGame;
        quitButton.clicked -= QuitGame;
        layoutButton.clicked -= LayoutConfig;
        volumeButton.clicked -= VolumeConfig;
    }
    private void VolumeConfig()
    {
       VolumeControll.InitVolumeControllToast();
    }

    private void LayoutConfig()
    {
        LayoutControll.InitLayoutControllToast();
    }

    private void QuitGame()
    {
        //ExitGame Toast
        QuitConfirmControll.InitQuitConfirmToast();
    }

    private void StartGame()
    {
        SoundController.Instance.PlayAudio("Start", SoundController.SoundChannel.SE);
        SceneManager.LoadSceneAsync("Site");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
