using MasterMemory;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[MemoryTable("InterviewerMaster"), MessagePackObject(true)]
public record InterviewerMaster
{
    [PrimaryKey]
    public string Interviewer_ID { get; set; }
    public string Interviewer_Path { get; set; }
    public string Interviewer_Reaction_Good { get; set; }
    public string Interviewer_Reaction_Normal { get; set; }
    public string Interviewer_Reaction_Bad { get; set; }
    //MasterMemory cannot read array,save raw
    public string Interviewer_Question { get; set; }


    [IgnoreMember]
    //Change raw Data
    public List<string> Interviewer_QuestionArray
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Interviewer_Question)) return new List<string>();
            return Interviewer_Question
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();
        }
    }
}