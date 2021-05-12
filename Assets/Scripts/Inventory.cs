using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;

    private Item[] itemList;
    private ServerController serverController;

    public Inventory()
    {
        itemList = new Item[40];

        for (int i = 0; i < 40; i++)
        {
            itemList[i] = Item.ClearItem();
        }

        serverController = GameObject.FindGameObjectWithTag("ServerController").GetComponent<ServerController>();
    }

    public Item[] GetItemList()
    {
        return itemList;
    }

    public void Update(List<JsonInventoryItem> items)
    {
        bool inventoryChanged = true;
        int j = 0;
        if (items!= null)
        {
            for (int i = 0; i < 40; i++)
            {
                {
                    if (j < items.Count && items[j].position == i)
                    {
                        itemList[i] = new Item(items[j].itemId, items[j].amount);
                        j++;
                    }
                    else
                    {
                        itemList[i] = Item.ClearItem();
                    }
                }
            }
        }
        if (inventoryChanged)
        {
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
