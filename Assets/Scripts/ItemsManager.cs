using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private const float DEFAULT_SCROLL_SPEED = 20f;

    public Sprite[] itemSprites;
    public Sprite[] itemSpritesMoving;
    [SerializeField] private Transform[] itemGroups;
    [SerializeField] private SlotMachine slotMachine;
    private Vector3[] itemGroupsFirstLocalPos;
    public int itemNum;
    private bool[] lockRotations;
    public float spacingBetweenItems;
    public float[] scrollSpeeds;

    private void Start()
    {
        lockRotations = new bool[itemGroups.Length]; //Control for slots.
        scrollSpeeds = new float[itemGroups.Length]; //Scroll speeds for slots.
        itemGroupsFirstLocalPos = new Vector3[itemGroups.Length]; //First positions of item groups.

        for (int i = 0; i < itemGroups.Length; i++)
        {
            itemGroupsFirstLocalPos[i] = itemGroups[i].localPosition;
        }

        for (int i = 0; i < lockRotations.Length; i++)
        {
            lockRotations[i] = true;
        }
        for (int i = 0; i < scrollSpeeds.Length; i++)
        {
            scrollSpeeds[i] = DEFAULT_SCROLL_SPEED; //We may update scrolls later.
        }
    }

    private void Update()
    {
        rotateItems(); //Regular rotation on itemGroups

    }

    private void rotateItems()
    {
        for (int i = 0; i < itemGroups.Length; i++)
        {
            if (!lockRotations[i])
                itemGroups[i].position -= new Vector3(0f, scrollSpeeds[i] * Time.deltaTime, 0f);
        }
    }

    public IEnumerator UnlockRotations()
    {
        resetSpeeds();
        for (int i = 0; i < lockRotations.Length; i++)
        {
            lockRotations[i] = false; //Starts moving
            itemGroups[i].GetComponent<ItemGroup>().setItemSprites(true); //Calls moving sprites
            float rand = Random.Range(0.1f, 0.4f);
            yield return new WaitForSeconds(rand);
        }
    }

    private void resetSpeeds()
    {
        for (int i = 0; i < scrollSpeeds.Length; i++)
        {
            scrollSpeeds[i] = DEFAULT_SCROLL_SPEED;
        }
    }

    public IEnumerator LockRotations(int[] targetItemIds)
    {

        for (int i = 0; i < lockRotations.Length; i++)
        {
            ItemGroup itemGroup = itemGroups[i].GetComponent<ItemGroup>();
            lockRotations[i] = true; //Locks rotation
            float delay = 0;
            if (i == 2 && slotMachine.result[0] == slotMachine.result[1])
            {
                StartCoroutine(speedDownScrollSpeed());
                delay = 1f;
            }
            StartCoroutine(itemGroup.rotateToTarget(itemGroups[i], targetItemIds[i], itemGroup, scrollSpeeds,delay,i)); 
            itemGroup.setItemSprites(false);
            float rand = Random.Range(0.6f, 1.2f);
            yield return new WaitForSeconds(rand);
        }
    }

    public void turnCompleted()
    {
        slotMachine.callCoinAnimation();
        slotMachine.isMachineRunning = false;
    }

    private IEnumerator speedDownScrollSpeed()
    {
        while (scrollSpeeds[2] >= 3.5f && slotMachine.isMachineRunning)
        {
            scrollSpeeds[2] -= 10f * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
    

}
