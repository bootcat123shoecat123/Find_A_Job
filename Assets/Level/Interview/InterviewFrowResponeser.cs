using UnityEngine;

public class InterviewFrowResponeser : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    InterviewFlowController interviewFlowController;
    void Start()
    {
        interviewFlowController = new InterviewFlowController();
         
    }
    public void StartNewTurn()
    {
        //show the question to the player
        interviewFlowController.DealQuestion(1);

        //wait for the player to watch the question and click the button to continue
        //deal three cards to the player
        interviewFlowController.DealCard(3);
  
    

    }
    
}
