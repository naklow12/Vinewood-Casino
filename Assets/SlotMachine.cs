using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private ItemsManager itemsManager;
    private bool isMachineRunning;

    public void runMachine()
    {
        if (!isMachineRunning)
            StartCoroutine(itemsManager.UnlockRotations());
    }
}
