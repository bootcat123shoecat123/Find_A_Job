using MasterMemory;
using MessagePack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[MemoryTable("InterviewReactionCardMaster"), MessagePackObject(true)]
public record InterviewReactionCardMaster
{
    [PrimaryKey]
    public string Card_ID { get; set; }
    public string Card_Name { get; set; }
    public string Card_Color { get; set; }
    public string Card_Detail { get; set; }
    public string Card_Text { get; set; }
    public string Card_Effect { get; set; }

    //MasterMemory cannot read array,save raw
    public string Card_Tag { get; set; }



    [IgnoreMember]
    //Change raw Data
    public List<E_Tag> Card_TagArray
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Card_Tag)) return new List<E_Tag>();
            return Card_Tag
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Select(s => Enum.Parse<E_Tag>(s))
                .ToList();
        }
    }
}