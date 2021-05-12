using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIInventory : MonoBehaviour
{
    
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private List<RectTransform> itemSlotList;
    private RectTransform deleteItemSlot;
    private RectTransform equipItemSlot;
    private RectTransform dropItemSlot;

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
        itemSlotList = new List<RectTransform>();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 32f;

        deleteItemSlot = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
        deleteItemSlot.gameObject.SetActive(true);
        deleteItemSlot.anchoredPosition = new Vector2(-43, 0);
        deleteItemSlot.GetComponent<ItemSlot>().position = 100;
        TextMeshProUGUI uiText1 = deleteItemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();
        uiText1.SetText("delete");

        equipItemSlot = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
        equipItemSlot.gameObject.SetActive(true);
        equipItemSlot.anchoredPosition = new Vector2(-43, -64);
        equipItemSlot.GetComponent<ItemSlot>().position = 101;
        TextMeshProUGUI uiText2 = equipItemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();
        uiText2.SetText("equip");

        dropItemSlot = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
        dropItemSlot.gameObject.SetActive(true);
        dropItemSlot.anchoredPosition = new Vector2(-43, -128);
        dropItemSlot.GetComponent<ItemSlot>().position = 102;
        TextMeshProUGUI uiText3 = dropItemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();
        uiText3.SetText("drop");

        Item[] items = inventory.GetItemList();
        for (int i = 0; i < 40; i++)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * -itemSlotCellSize);
            itemSlotRectTransform.GetComponent<ItemSlot>().position = i;
            itemSlotList.Add(itemSlotRectTransform);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = items[i].GetSprite();
            itemSlotRectTransform.Find("Image").GetComponent<DragAndDrop>().position = i;
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (items[i].amount > 1)
            {
                uiText.SetText(items[i].amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
        }

/*        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * -itemSlotCellSize);
            itemSlotList.Add(itemSlotRectTransform);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if (x > 7)
            {
                x = 0;
                y++;
            }
        }*/

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {

        Item[] items = inventory.GetItemList();
        for (int i = 0; i < 40; i++)
        {
            Image image = itemSlotList[i].Find("Image").GetComponent<Image>();
            image.sprite = items[i].GetSprite();

            TextMeshProUGUI uiText = itemSlotList[i].Find("AmountText").GetComponent<TextMeshProUGUI>();
            if (items[i].amount > 1)
            {
                uiText.SetText(items[i].amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
        }
    }
}
