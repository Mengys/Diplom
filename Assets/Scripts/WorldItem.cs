using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldItem : MonoBehaviour
{

    public JsonDrop drop;
    public static WorldItem SpawnWorldItem(Vector3 position, Item item, int amount)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfWorldItem, position, Quaternion.identity);

        WorldItem worldItem = transform.GetComponent<WorldItem>();
        worldItem.SetItem(item);
        worldItem.SetAmount(item, amount);

        return worldItem;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("AmountText").GetComponent<TextMeshPro>();
    }

    public void SetAmount(Item item, int amount)
    {
        item.amount = amount;
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
