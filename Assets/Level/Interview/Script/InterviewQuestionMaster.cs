using MasterMemory;
using MessagePack;
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
    public List<E_Tag> Question_Good { get; set; }
    public List<E_Tag> Question_Bad { get; set; }

}
