using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Doors : MonoBehaviour
{
    [SerializeField] Image Enterract;
    [SerializeField] Image door;
    [SerializeField] Image roof;

    private void Awake()
    {
        Enterract.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !Enterract.enabled)
        {
            Enterract.enabled = true;

            if (Input.GetKey(KeyCode.E))
            {
                door.enabled = false;
                roof.enabled = false;
            }
        }
        else if (Enterract.enabled)
        {
            door.enabled = true;
            roof.enabled = true;
        }
    }
}
