using MasterMemory;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
[MemoryTable("InterviewQuestionMaster"),MessagePackObject(true)]
public record InterviewQuestionMaster
{
    

    [PrimaryKey]
    public string Question_ID { get; set; }
    public string Question_Sentence { get; set; }

    //MasterMemory cannot read array,save raw
    public string Question_Good { get; set; }
    public string Question_Bad { get; set; }

    [IgnoreMember]
    //Change raw Data
    public E_Tag[] Question_GoodArray
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Question_Good)) return Array.Empty<E_Tag>();
            return Question_Good
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Select(s => Enum.Parse<E_Tag>(s))
                .ToArray();
        }
    }
    public E_Tag[] Question_BadArray
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Question_Good)) return Array.Empty<E_Tag>();
            return Question_Good
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Select(s => Enum.Parse<E_Tag>(s))
                .ToArray();
        }
    }


}
