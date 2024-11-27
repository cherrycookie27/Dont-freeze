using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickupable
{
    public int id;
    public void PickUp()
    {
        bool b = Inventory.instance.PickUp(id);
        if (b)
        {
            Destroy(gameObject);
        }
    }
}
