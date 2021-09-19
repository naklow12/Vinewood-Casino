using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler
{
    public static void schedule()
    {
        Possibility[] possibilities = Possibilities.possibilityArr;
        int[] frequencyArr = new int[possibilities.Length];
        int[] order;
        int totalPos = 0;
        for (int i = 0; i < possibilities.Length; i++)
        {
            totalPos += possibilities[i].possibilityRate;
        }
        order = new int[totalPos];

        for (int i = 0; i < frequencyArr.Length; i++) //To get frequency in total
        {
            frequencyArr[i] = totalPos / possibilities[i].possibilityRate;
        }

        for (int i = 0; i < order.Length; i++) //BOX
        {
            List<int> possibleElements = new List<int>();
            for (int j = 0; j < frequencyArr.Length; j++) //ELEMENTS
            {
                if(isSetAvailable(i,frequencyArr[j],order,j))
                    possibleElements.Add(j);
            }

            int id = checkForPriority(possibleElements, frequencyArr,i);

            order[i] = id;
            //TO-DO: Set Order value

            ScheduleFile.SaveToFile(new ScheduleDataset(order));
            //TO-DO: Save Order List by ScheduleFile
        }


    }

    private static int checkForPriority(List<int> possibleElements, int[] frequencyArr, int num)
    {
        int[] weightArr = new int[possibleElements.Count];
        for (int i = 0; i < possibleElements.Count; i++)
        {
            int frequency = frequencyArr[possibleElements[i]];
            int topLimit = ((num / frequency) * frequency) + frequency;

            weightArr[i] = topLimit - num;
        }

        int min = -1;
        int minId=0;
        for (int i = 0; i < weightArr.Length; i++)
        {
            if(weightArr[i] < min || min == -1)
            {
                min = weightArr[i];
                minId = possibleElements[i]; //Returns possible element id on first possibility array.
            }
        }

        return minId;
    }

    private static bool isSetAvailable(int num,int frequency, int[] order, int id) //Checks is set used in frequency range
    {
        bool isAvailable = true;
        int botLimit = (num / frequency) * frequency;
        int topLimit = botLimit + frequency;
        if (topLimit > order.Length)
            topLimit = order.Length;

        bool isUsed = false;
        for (int i = botLimit; i < topLimit; i++)
        {
            if (order[i] == id)
                isUsed = true;
        }

        if (isUsed)
            isAvailable = false;


        return isAvailable;
    }


}
