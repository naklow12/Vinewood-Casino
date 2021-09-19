using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler
{
    public static void schedule()
    {
        Possibility[] possibilities = Possibilities.possibilityArr;
        int[] frequencyArr = new int[possibilities.Length];
        int[] order; //To save scheduled numbers.
        int totalPos = 0;
        for (int i = 0; i < possibilities.Length; i++)
        {
            totalPos += possibilities[i].possibilityRate;
        }
        order = new int[totalPos];

        for (int i = 0; i < order.Length; i++) //Shouldn't be 0 for future comparison. (We have 0th id)
        {
            order[i] = -1;
        }

        for (int i = 0; i < frequencyArr.Length; i++) //To get frequency in total
        {
            frequencyArr[i] = Mathf.RoundToInt((float)totalPos / possibilities[i].possibilityRate);
        }

        for (int i = 0; i < order.Length; i++) //BOX FILLING
        {
            List<int> possibleElements = new List<int>();
            for (int j = 0; j < frequencyArr.Length; j++) //ELEMENTS
            {
                if(isSetAvailable(i,frequencyArr[j],order,j))
                    possibleElements.Add(j);
            }

            int id = checkForPriority(possibleElements, frequencyArr,i); //Get most priority required ID

            order[i] = id; //Set ID to order

            ScheduleFile.SaveToFile(new ScheduleDataset(order)); //Save Order List for future comparison
        }


    }

    private static int checkForPriority(List<int> possibleElements, int[] frequencyArr, int num)
    {
        int[] weightArr = new int[possibleElements.Count];
        for (int i = 0; i < possibleElements.Count; i++)
        {
            int frequency = frequencyArr[possibleElements[i]];
            int topLimit = ((num / frequency) * frequency) + frequency;

            weightArr[i] = topLimit - num; //To give weight number. Lower has more priority.
        }

        int min = -1;
        int minId=0;
        List<int> sameWeightIds = new List<int>(); //To use if we have same priorities randomize between them.
        for (int i = 0; i < weightArr.Length; i++)
        {
            if(weightArr[i] < min || min == -1) //If we don't have min value yet or found a lower value
            {
                min = weightArr[i];
                sameWeightIds.Clear(); //CLEAR if a new minimum value found
                sameWeightIds.Add(possibleElements[i]);
                minId = possibleElements[i]; //Returns possible element id on first possibility array.
            }else if (weightArr[i] == min) //If it has same priority we add to sameWeightsId
            {
                sameWeightIds.Add(possibleElements[i]);
            }
        }
        if (sameWeightIds.Count > 1)//If we have more than one element in it.
            minId = sameWeightIds[Random.Range(0, sameWeightIds.Count)];

        return minId;
    }

    private static bool isSetAvailable(int num,int frequency, int[] order, int id) //Checks is set used in frequency range
    {
        bool isAvailable = true;
        int botLimit = (num / frequency) * frequency;
        int topLimit = botLimit + frequency;
        if (topLimit > order.Length)
            topLimit = order.Length;

        for (int i = botLimit; i < topLimit; i++) //Searching between limits if same id exists.
        {
            if (order[i] == id)
                isAvailable = false;
        }

        return isAvailable;
    }


}
