using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct itemData
{
    public int id;
    public Sprite icon;
    public string itemName;
}
public class InventorySlot : MonoBehaviour
{
    Player player;
    public int id;
    public Image spriteImage;
    public itemData data;

    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<Player>();
        spriteImage = transform.Find("IconImage").GetComponent<Image>();
        spriteImage.gameObject.SetActive(false);
    }
    public void Add(itemData newData)
    {
        if (newData.id != 0)
        {
            data = newData;
            spriteImage.gameObject.SetActive(true);
            spriteImage.sprite = data.icon;
            id = data.id;
        }
    }

    public void Use()
    {
        switch(id)
        {
            case 0: default:
                
                break;
            case 1:
                player.Heal(2);
                Clear();
                break;
            case 2:
                
                break;
            case 3:

                break;
        }
    }

    private void Clear()
    {
        data = new itemData();
        spriteImage.gameObject.SetActive(false);
        id = 0;
    }
    public void ReSize(float size)
    {
        GetComponent<Image>().rectTransform.localScale = new Vector2(size, size);
    }
}