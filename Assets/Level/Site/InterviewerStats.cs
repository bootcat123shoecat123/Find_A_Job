using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class InterviewerStats
{
    

    public void SetInterviewerStats(string change, E_Successfulness successfulness)
    {
        float addition = 0;
        switch (successfulness)
        {
            case E_Successfulness.Good:
                addition = 2.5f;
                break;
            case E_Successfulness.Normal:
                addition = 1.0f;
                break;
            case E_Successfulness.Bad:
                addition = -2.0f;
                break;
            default:
                break;
        }

        // Split the change string into a list of strings
        List<string> changeItem = change.Split(',').ToList<string>();
        int changeValue = 0;
        foreach (string item in changeItem)
        {
            changeValue = int.Parse(item.Substring(1));
            InterviewFlowController.instance.InterviewChangedValue(item[0], changeValue, addition);

        }
    }
}