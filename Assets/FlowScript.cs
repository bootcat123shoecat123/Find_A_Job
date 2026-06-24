using System;
using UnityEngine;

public class FlowScript
{
    public enum E_JobTimeEnum
    {
        Elector,
        AI,
        Steam
    }
    public enum E_SitePages
    {
          Search,Create,PartTime,Interview,Sleep
    }
    public void PlayTrial()
    {
        UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }

    public int ChoiseEra()
    {
        return UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }
   
    public void SwitchSitePage(E_SitePages pageID)
    {
        switch (pageID)
        {
            case E_SitePages.Search:

                break;
            case E_SitePages.Create:
                
                break;
            case E_SitePages.PartTime:
                
                break;
            case E_SitePages.Interview:
                
                break;
            case E_SitePages.Sleep:
                
                break;
            default:
                
                break;
        }

        UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }
    public void ActiveSearch()
    {
        UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }
    public void ActiveCreate()
    {
        UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }
    public void ActivePartTime()
    {
        UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }
    public void ActiveSleep()
    {
        //Go next day
    }

    public void ActiveInterview()
    {
        UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_JobTimeEnum)).Length);
    }

}