using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public float zAngle;
    private Transform door;

    private void Start()
    {
        door = gameObject.transform;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.transform.Rotate(new Vector3(0, 0, zAngle), Space.Self);  
        }
    }
}
