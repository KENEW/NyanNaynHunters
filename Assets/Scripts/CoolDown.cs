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

    public void Restart()
    {
        StartCoroutine("RestartCoroutine");
    }

    IEnumerator RestartCoroutine()
    {
        while (slider.value > 0)
        {
            slider.value -= valuePerSecond * Time.deltaTime;
            yield return null;
        }
    }

    public float GetSliderValue()
    {
        return slider.value;
    }
}
