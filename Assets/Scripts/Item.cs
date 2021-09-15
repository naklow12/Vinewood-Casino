using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public int id;
    public GameObject gameObj;
    public SpriteRenderer spriteRenderer;
    
    public Item(int id, GameObject gameObj, SpriteRenderer sprite)
    {
        this.id = id;
        this.gameObj = gameObj;
        this.spriteRenderer = sprite;
    }

    public void setSprite(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
    }

}
