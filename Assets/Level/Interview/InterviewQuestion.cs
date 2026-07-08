using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
[MemoryTable("InterviewQuestionMaster"),MessagePackObject(true)]
public class InterviewQuestion
{
    public InterviewQuestion(string question_ID, string question_Sentence, List<E_Tag> question_Good, List<E_Tag> question_Normal, List<E_Tag> question_Bad)
    {
        Question_ID = question_ID;
        Question_Sentence = question_Sentence;
        Question_Good = question_Good;
        Question_Normal = question_Normal;
        Question_Bad = question_Bad;
    }
    // input value by this
    public InterviewQuestion(string id, string question, string goodTag, string normalTag, string badTag)
    {
        Question_ID = id;
        Question_Sentence = question;
        goodTag.Split(", ").ToList().ForEach(
            x => Question_Good.Add((E_Tag)System.Enum.Parse(typeof(E_Tag), x))
            );
        normalTag.Split(", ").ToList().ForEach(
            x => Question_Normal.Add((E_Tag)System.Enum.Parse(typeof(E_Tag), x))
            );
        badTag.Split(", ").ToList().ForEach(
            x => Question_Bad.Add((E_Tag)System.Enum.Parse(typeof(E_Tag), x))
            );
    }
    


    [PrimaryKey]
    public string Question_ID { get; }
    public string Question_Sentence { get; }
    public List<E_Tag> Question_Good { get; }
    public List<E_Tag> Question_Normal { get; }
    public List<E_Tag> Question_Bad { get; }

}
