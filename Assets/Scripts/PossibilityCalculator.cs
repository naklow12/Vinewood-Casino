using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibilityCalculator : MonoBehaviour
{
    int totalPossibility;

    void Start()
    {
        generatePossibilities();
        int[] order = getRandomResult();
    }

    private void generatePossibilities()
    {
        for (int i = 0; i < Possibilities.possibilityArr.Length; i++)
        {
            int possibilityRate = Possibilities.possibilityArr[i].possibilityRate;
            totalPossibility += possibilityRate;
            Possibilities.possibilityArr[i].possibilityRate = totalPossibility;
        }
    }

    private int[] getRandomResult()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int rand = Random.Range(0, totalPossibility);
        int arrayNum = -1;
        for (int i = 0; i < Possibilities.possibilityArr.Length; i++)
        {
            if (Possibilities.possibilityArr[i].possibilityRate <= rand)
                arrayNum = i;
        }
        return Possibilities.possibilityArr[arrayNum+1].order;
    }


}
