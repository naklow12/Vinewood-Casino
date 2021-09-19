using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGroup : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    private int itemNum;
    [SerializeField] private GameObject itemGo;
    [SerializeField] ItemsManager itemsManager;
    private int moveCounter;

    private void Start()
    {
        items = new List<Item>();
        itemNum = itemsManager.itemNum;
        Transform itemTrans = itemGo.transform;
        itemTrans.localPosition = new Vector3(itemTrans.localPosition.x, 0f, itemTrans.localPosition.z);
        generateItems();
    }

    private void generateItems() //Generate items for first time.
    {
        for (int i = itemNum-1; i >= 0; i--)
        {
            GameObject go = Instantiate(itemGo, transform);
            go.transform.localPosition = new Vector3(0f, go.transform.localPosition.y + (i * itemsManager.spacingBetweenItems), 0f);
            go.SetActive(true);
            Item item = new Item(i, go, go.GetComponent<SpriteRenderer>());
            items.Add(item);
            item.setSprite(itemsManager.itemSprites[i]);
        }
    }

    private void moveItemToTop(Transform transform)
    {
        transform.localPosition = new Vector3(transform.localPosition.x,itemsManager.spacingBetweenItems * (itemNum - 2 + moveCounter),transform.localPosition.z);
    }

    private void relistLastItem() //Sending last item to top of the list
    {
        moveCounter++;
        List<Item> tempList = new List<Item>();
        tempList.Add(items[items.Count-1]);
        for (int i = 0; i < items.Count-1; i++)
        {
            tempList.Add(items[i]);
        }
        items = tempList;
        moveItemToTop(items[0].gameObj.transform); //Send the item to the top
    }

    public void setItemSprites(bool isMoving)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (isMoving)
            {
                items[i].spriteRenderer.sprite = itemsManager.itemSpritesMoving[items[i].id];
            }
            else
            {
                items[i].spriteRenderer.sprite = itemsManager.itemSprites[items[i].id];
            }
        }
        
    }

    public int getCurrentItemNum()
    {
        return items[3].id;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.localPosition.y) > (items[itemNum-2].gameObj.transform.localPosition.y) + itemsManager.spacingBetweenItems) //Calculating 2 item size down item (unseen) by scroll size.
        {
            relistLastItem();
        }
    }
}
