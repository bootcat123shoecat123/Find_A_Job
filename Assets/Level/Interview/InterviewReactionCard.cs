using MasterMemory;
using MessagePack;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[MemoryTable("InterviewReactionCard"), MessagePackObject(true)]
public class InterviewReactionCard: ScriptableObject
{
    public InterviewReactionCard(string card_ID, string card_Name, string card_Color, string card_Detail, string card_Text, string card_Effect, List<E_Tag> card_Tag, bool card_Bad)
    {
        Card_ID = card_ID;
        Card_Name = card_Name;
        Card_Color = card_Color;
        Card_Detail = card_Detail;
        Card_Text = card_Text;
        Card_Effect = card_Effect;
        Card_Tag = card_Tag;
        Card_Bad = card_Bad;
    }
    public InterviewReactionCard(string card_ID, string card_Name, string card_Color, string card_Detail, string card_Text, string card_Effect, string card_Tag, string card_Bad=null)
    {
        Card_ID = card_ID;
        Card_Name = card_Name;
        Card_Color = card_Color;
        Card_Detail = card_Detail;
        Card_Text = card_Text;
        Card_Effect = card_Effect;
        card_Tag.Split(", ").ToList().ForEach(
            x => Card_Tag.Add((E_Tag)System.Enum.Parse(typeof(E_Tag), x))
            );
        Card_Bad = string.IsNullOrEmpty(card_Bad);
    }

    [PrimaryKey]
    public string Card_ID { get; }
    public string Card_Name { get; }
    public string Card_Color { get; }
    public string Card_Detail { get; }
    public string Card_Text { get; }
    public string Card_Effect { get; }
    public List<E_Tag> Card_Tag { get; }
    public bool Card_Bad { get; }
}