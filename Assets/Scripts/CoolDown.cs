using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoolDown : MonoBehaviour
{
    private Slider slider;
    public int valuePerSecond;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = slider.maxValue;
    }
    void Update()
    {
        if (slider.value > 0)
        {
            slider.value -= valuePerSecond * Time.deltaTime;
        }
        else
        {
            slider.value = slider.maxValue;
        }
    }
}
