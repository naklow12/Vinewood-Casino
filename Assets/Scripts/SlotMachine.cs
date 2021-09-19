using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private ItemsManager itemsManager;
    [SerializeField] private PossibilityCalculator possibilityCalculator;
    private bool isMachineRunning;
    private const float stopCount = 2f;

    public void runMachine()
    {
        if (!isMachineRunning)
        {
            StartCoroutine(itemsManager.UnlockRotations());
            int[] result = possibilityCalculator.getRandomResult();
            Debug.Log("Res 1 = " + result[0] + "Res 2 = " + result[1] + "Res 3 = " + result[2]);
            isMachineRunning = true;
        }
    }

    IEnumerator stopMachine()
    {
        yield return new WaitForSeconds(stopCount);

    }
}
