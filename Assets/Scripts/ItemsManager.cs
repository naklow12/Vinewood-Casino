using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public Sprite[] itemSprites;
    public Sprite[] itemSpritesMoving;
    [SerializeField] private Transform[] itemGroups;
    public int itemNum;
    private bool[] lockRotations;
    public float spacingBetweenItems;
    private const float DEFAULT_SCROLL_SPEED = 15f;
    private float[] scrollSpeeds;

    private void Start()
    {
        lockRotations = new bool[itemGroups.Length]; //Control for slots.
        scrollSpeeds = new float[itemGroups.Length]; //Scroll speeds for slots.
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

}
