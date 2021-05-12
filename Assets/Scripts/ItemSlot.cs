using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int position;
    private RectTransform rectTransform;
    Vector2 defaultPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPosition = rectTransform.anchoredPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragAndDrop>().defaultPosition;

            switch (position)
            {
                case 100:
                    ServerController.DeleteItem(eventData.pointerDrag.GetComponent<DragAndDrop>().position);
                    Debug.Log("deleteitem");
                    break;
                case 101:
                    ServerController.EquipItem(eventData.pointerDrag.GetComponent<DragAndDrop>().position);
                    Debug.Log("equip");
                    break;
                case 102:
                    ServerController.DropItem(eventData.pointerDrag.GetComponent<DragAndDrop>().position);
                    Debug.Log("DropItem");
                    break;
                default: 
                    ServerController.MoveItem(eventData.pointerDrag.GetComponent<DragAndDrop>().position, position, 1);
                    break;
            }
        }
    }
}
