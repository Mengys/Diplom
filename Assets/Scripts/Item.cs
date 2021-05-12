using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int amount;
    public long id;


    public Item(long id, int amount)
    {
        this.id = id;
        this.amount = amount;
    }

    public Sprite GetSprite()
    {
        switch (id)
        {
            default:
            case -1: return null;
            case 0: return ItemAssets.Instance.swordSprite;
            case 1: return ItemAssets.Instance.bowSprite;
            case 2: return ItemAssets.Instance.healthPotionSprite;
        }
    }

    public bool IsStackable()
    {
        switch(id)
        {
            default:
            case 2:
                return true;
            case 1:
            case 0:
                return false;
        }
    }

    public static Item ClearItem()
    {
        return new Item(-1, 0);
    }
}
