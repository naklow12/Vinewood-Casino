using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibilityCalculator : MonoBehaviour
{
    private int totalPossibility;
    private int[] historyData;
    private Possibility[] livePossibilityArr;

    void Start()
    {
        Scheduler.schedule();
        ScheduleDataset dataSet = ScheduleFile.LoadSave();
        if(dataSet != null)
            historyData = dataSet.scheduleDataArr;
        livePossibilityArr = Possibilities.possibilityArr; //I kept the original to use it later.
        generatePossibilities();
    }

    private void generatePossibilities()
    {
        for (int i = 0; i < livePossibilityArr.Length; i++)
        {
            int possibilityRate = livePossibilityArr[i].possibilityRate;
            totalPossibility += possibilityRate;
            livePossibilityArr[i].possibilityRate = totalPossibility;
        }
    }

    public int[] getRandomResult()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int rand = Random.Range(0, totalPossibility);
        int arrayNum = -1;
        for (int i = 0; i < livePossibilityArr.Length; i++)
        {
            if (livePossibilityArr[i].possibilityRate <= rand)
                arrayNum = i;
        }

        return livePossibilityArr[arrayNum+1].order;
    }




}
