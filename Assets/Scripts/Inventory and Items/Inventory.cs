using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Image> images;
    int index = 0;
    public float selectHighlight = 1f;
    public float normalSize = 0.85f;

    private void Start()
    {
        foreach (Image image in images)
        {
            image.rectTransform.localScale = new Vector2(normalSize, normalSize);
        }
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            index = (index - 1 + images.Count) % images.Count;
            UpdateHighlight();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            index = (index + 1) % images.Count;
            UpdateHighlight();
        }
    }

    private void UpdateHighlight()
    {
        foreach (Image image in images)
        {
            image.rectTransform.localScale = new Vector2(normalSize, normalSize);
        }

        images[index].rectTransform.localScale = new Vector2(selectHighlight, selectHighlight);
        //make the active image have a boolean for usage
    }
    //put item pictures to right boxes (or managers jobs?)
}
