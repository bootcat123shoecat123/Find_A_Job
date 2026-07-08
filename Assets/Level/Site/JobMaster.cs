using MasterMemory;
using MessagePack;
using System;
using UnityEngine;
[MemoryTable("JobMaster"), MessagePackObject(true)]
public class JobMaster : ScriptableObject
{

    public JobMaster(string job_ID, string job_Company, string job_Work, string job_Loc, string job_Tel, Texture2D job_Image, Texture2D job_Interviewer, E_Category job_Category)
    {
        Job_ID = job_ID;
        Job_Company = job_Company;
        Job_Work = job_Work;
        Job_Loc = job_Loc;
        Job_Tel = job_Tel;
        Job_Image = job_Image;
        Job_Interviewer = job_Interviewer;
        Job_Category = job_Category;
    }
    // input value by this
    public JobMaster(string job_ID, string job_Company, string job_Work, string job_Loc, string job_Tel, string job_Image, string job_Interviewer, string job_Category)
    {
        Job_ID = job_ID;
        Job_Company = job_Company;
        Job_Work = job_Work;
        Job_Loc = job_Loc;
        Job_Tel = job_Tel;
        Job_Image = Resources.Load<Sprite>( job_Image).texture;
        Job_Interviewer = Resources.Load<Sprite>(job_Interviewer).texture;
        Job_Category = (E_Category)Enum.Parse(typeof(E_Category), job_Category);
    }
    
    [PrimaryKey]
    public string Job_ID { get; }
    public string Job_Company { get; }
    public string Job_Work { get; }
    public string Job_Loc { get; }
    public string Job_Tel { get; }
    public Texture2D Job_Image { get; }
    public Texture2D Job_Interviewer { get; }
    public E_Category Job_Category { get; }

}