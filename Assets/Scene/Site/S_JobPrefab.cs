using System;
using UnityEngine;

[CreateAssetMenu(fileName = "S_JobPrefab", menuName = "Scriptable Objects/S_JobPrefab")]
public class S_JobPrefab : ScriptableObject
{
    public Job job;


}
[Serializable]
public struct Job
{
    public string s_Company;
    public string s_Work;
    public string s_Loc;
    public string s_Tel;
    public Texture2D t_Image;
    public Texture2D t_Interviewtor;
    public E_Category e_Category;

}