using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezingSlider : MonoBehaviour
{
    public static FreezingSlider instance;
    public Slider freexingSlider;
    public float maxValue = 80f;
    public float valuePerFoodCan = 10f;

    private float currentFreezeValue;
    private bool isFreezingActive = false;

    private void Start()
    {
        currentFreezeValue = maxValue;
        UpdateSlider();
    }

    private void Update()
    {
        if (currentFreezeValue > 0)
        {
            currentFreezeValue -= Time.deltaTime;
            UpdateSlider();
        }
        else
        {
            if(!isFreezingActive)
            {
                StartCoroutine(Freezing());
            }
        }
    }

    IEnumerator Freezing()
    {
        isFreezingActive = true;
        while (currentFreezeValue <= 0)
        {
            Player.Instance.TakeDamage(2);
            yield return new WaitForSeconds(2f);
        }
        isFreezingActive = false;
    }
    public void UpdateSlider()
    {
        freexingSlider.value = currentFreezeValue / maxValue;
    }

    public void Eating()
    {
        currentFreezeValue += valuePerFoodCan;
        if (currentFreezeValue > maxValue)
            currentFreezeValue = maxValue;

        if(currentFreezeValue > 0)
        {
            StopCoroutine(Freezing());
        }
    }
}
