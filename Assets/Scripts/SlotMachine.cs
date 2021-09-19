using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private ItemsManager itemsManager;
    [SerializeField] private PossibilityCalculator possibilityCalculator;
    private bool isMachineRunning;
    private const float stopCount = 2f;
    public int[] result;

    public void runMachine()
    {
        if (!isMachineRunning)
        {
            StartCoroutine(itemsManager.UnlockRotations());
            result = possibilityCalculator.getNextResult();
            PlayerPrefs.SetInt("spinCount",PlayerPrefs.GetInt("spinCount")+1);
            Debug.Log("Res 1 = " + result[0] + " Res 2 = " + result[1] + " Res 3 = " + result[2]);
            isMachineRunning = true;
            StartCoroutine(stopMachine());
        }
    }

    IEnumerator stopMachine()
    {
        yield return new WaitForSeconds(stopCount);
        StartCoroutine(itemsManager.LockRotations(result));
    }
}
