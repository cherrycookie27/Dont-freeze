using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private AudioSource eating;
    InventoryManager inventoryManager;
    FreezingSlider freezingSlider;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventoryManager.AddItem();
        }
    }

    public void UseItem()
    {
        if (/*itemActive &&*/Input.GetKey(KeyCode.E))
        {
            freezingSlider.Eating();
        }
    }
    //if item active in the inventory make it be able to be used
}
