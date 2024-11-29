using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inside : MonoBehaviour
{
    [SerializeField] FreezingSlider slider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            slider.inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            slider.inside = false;
        }
    }
}
