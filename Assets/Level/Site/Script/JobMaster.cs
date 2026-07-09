using MasterMemory;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MemoryTable("JobMaster"), MessagePackObject(true)]
public record JobMaster
{
    [PrimaryKey]
    public string Job_ID { get; set; }

    public string Job_Company { get; set; }
    public E_Category Job_Category { get; set; }
    public string Job_Work { get; set; }
    public string Job_Loc { get; set; }
    public string Job_Tel { get; set; }
    public string Job_ImagePath { get; set; }
    public string Job_Interviewer { get; set; }
}