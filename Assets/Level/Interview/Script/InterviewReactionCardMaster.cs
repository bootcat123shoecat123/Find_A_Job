using MasterMemory;
using MessagePack;
using NUnit.Framework;
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
    public List<E_Tag> Card_Tag { get; set; }
}