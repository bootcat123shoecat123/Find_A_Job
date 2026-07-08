using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LayoutControll : ToastBase
{
    Button rejectButton, acceptButton;
    RadioButtonGroup layoutGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        var root = GetComponent<UIDocument>().rootVisualElement;
        rejectButton = root.Q<Button>("Reject");
        acceptButton = root.Q<Button>("Accept");
        layoutGroup = root.Q<RadioButtonGroup>("SizeGroup");
        
    }
    private void OnEnable()
    {
        if (rejectButton != null)
            rejectButton.clicked += BackPrev;
        if (acceptButton != null)
            acceptButton.clicked += ChangeConfig;
    }

    private void OnDisable()
    {
        if (rejectButton != null)
            rejectButton.clicked -= BackPrev;
        if (acceptButton != null)
            acceptButton.clicked -= ChangeConfig;
    }

    public static void InitLayoutControllToast()
    {
        CreateToast(Resources.Load("Prefabs/Toast/LayoutControllPrefab"));
    }

    private void ChangeConfig()
    {
        // Change the layout size based on the selected radio button
        switch (layoutGroup.value)
        {
            case (int)E_LayoutSize.Full:
                Screen.fullScreen = true; 
                break;
            case (int)E_LayoutSize.FHD:
                
                Screen.SetResolution(1920, 1080, false);
                break;
            default:
                break;
        }
        //Else configure
    }

    private void BackPrev()
    {
        if (rejectButton != null)
        {
            rejectButton.clicked -= BackPrev;
        }
        DestroyToast();
    }

}
