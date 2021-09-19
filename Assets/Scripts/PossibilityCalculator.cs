using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibilityCalculator : MonoBehaviour
{
    private int totalPossibility;
    private int[] historyData;

    void Start()
    {
        Scheduler.schedule();
        ScheduleDataset dataSet = ScheduleFile.LoadSave();
        if(dataSet != null)
            historyData = dataSet.scheduleDataArr;
    }

    public int[] getNextResult()
    {
        
        return Possibilities.possibilityArr[historyData[PlayerPrefs.GetInt("spinCount")]].order;
    }




}
