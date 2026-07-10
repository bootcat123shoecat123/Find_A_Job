using System.Collections.Generic;

public class InterviewQuestionDeck
{
    public static InterviewQuestionDeck instance { get; internal set; }
    public List<InterviewQuestionMaster> questions;
    public void InitInterviewQuestionDeck()
    {
        questions = new List<InterviewQuestionMaster>();
    }
}