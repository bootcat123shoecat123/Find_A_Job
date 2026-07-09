using SupSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeControll : ToastBase
{
    Button rejectButton, acceptButton;
    Slider masterVolume,bgmVolume,seVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        
        
    }
    
    private void OnEnable()
    {
        //Button event registration
        var root = GetComponent<UIDocument>().rootVisualElement;
        rejectButton = root.Q<Button>("B_Reject");
        acceptButton = root.Q<Button>("B_Accept");
        if (rejectButton != null)
            rejectButton.clicked += BackPrev;
        if (acceptButton != null)
            acceptButton.clicked += ChangeConfig;

        //VolumeSlider event registration
        masterVolume = root.Q<Slider>("S_MasterVolumeSlider");
        bgmVolume = root.Q<Slider>("S_BGMVolumeSlider");
        seVolume = root.Q<Slider>("S_SEVolumeSlider");
        masterVolume.value=SoundController.Instance.GetVolume(SoundController.SoundChannel.Master);
        bgmVolume.value=SoundController.Instance.GetVolume(SoundController.SoundChannel.BGM);
        seVolume.value=SoundController.Instance.GetVolume(SoundController.SoundChannel.SE);
        
    }

    private void OnDisable()
    {
        if (rejectButton != null)
            rejectButton.clicked -= BackPrev;
        if (acceptButton != null)
            acceptButton.clicked -= ChangeConfig;
    }

    private void ChangeConfig()
    {
        // Change the layout size based on the selected radio button
        VolumeChange(SoundController.SoundChannel.Master, masterVolume.value);
        VolumeChange(SoundController.SoundChannel.BGM, bgmVolume.value);
        VolumeChange(SoundController.SoundChannel.SE, seVolume.value);
        SoundController.Instance.PlayAudio("Accept", SoundController.SoundChannel.SE);
        //Else configure
    }

    private void BackPrev()
    {

        SoundController.Instance.PlayAudio("Reject", SoundController.SoundChannel.SE);
        if (rejectButton != null)
        {
            rejectButton.clicked -= BackPrev;
        }
        DestroyToast();
    }

    internal static void InitVolumeControllToast()
    {
        CreateToast(Resources.Load("Prefabs/Toast/VolumeControllPrefab"));
    }

    // Update is called once per frame
    public void VolumeChange(SoundController.SoundChannel volumeName,float volume)
    {
        SoundController.Instance.ControllMixerVolume(volumeName, volume);
    }
}
