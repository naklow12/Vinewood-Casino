using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibilityCalculator : MonoBehaviour
{
    private int[] historyData;
    [SerializeField] bool checkScheduledData;

    void Start()
    {
        if(ScheduleFile.LoadSave() == null)
            Scheduler.schedule();
        ScheduleDataset dataSet = ScheduleFile.LoadSave();
        if(dataSet != null)
            historyData = dataSet.scheduleDataArr;
        if (checkScheduledData)
            testSchedule();
    }

    public int[] getNextResult()
    {
        
        return Possibilities.possibilityArr[historyData[PlayerPrefs.GetInt("spinCount")%historyData.Length]].order;
    }

    public void testSchedule()
    {
        Possibility[] possibilities = Possibilities.possibilityArr;
        int totalPossibilities = getTotalPossibilities(); 
        for (int i = 0; i < possibilities.Length; i++)
        {
            int lastArrNum = -1;
            int frequency = Mathf.RoundToInt((float) totalPossibilities / possibilities[i].possibilityRate);
            bool isFrequencyTestOK = true;
            int ctr=0;
            for (int j = 0; j < historyData.Length; j++)
            {
                if (i == historyData[j])
                {
                    if (lastArrNum != -1 && frequency < (j - lastArrNum))
                    {
                        isFrequencyTestOK = false;
                        lastArrNum = j;
                    }
                    ctr++;
                }
            }
            Debug.Log("Partial Frequency Test OK for "+ i +"? " + isFrequencyTestOK);
            Debug.Log("Total frequency test (in 100) OK for " + i + "? " + (ctr==possibilities[i].possibilityRate));
        }
    }

    private int getTotalPossibilities()
    {
        int totalPossibilities = 0;
        for (int i = 0; i < Possibilities.possibilityArr.Length; i++)
        {
            totalPossibilities = Possibilities.possibilityArr[i].possibilityRate;
        }
        return totalPossibilities;
    }


}
