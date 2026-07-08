using UnityEngine;
using UnityEngine.UIElements;

public class SiteResponeController : MonoBehaviour
{
    Button jobListButton, partTimeButton,titleButton,interviewListButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        jobListButton = root.Q<Button>("JobList");
        partTimeButton = root.Q<Button>("partTime");
        titleButton = root.Q<Button>("Title");
        interviewListButton = root.Q<Button>("InterviewList");
    }
}
