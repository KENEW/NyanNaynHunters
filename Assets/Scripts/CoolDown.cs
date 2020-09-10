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
        slider.maxValue = TBL_GAME_SETTING.GetEntity(0).CardSelectTime;
        slider.maxValue = 100f;
        slider.value = slider.maxValue;
    }

    public void Restart()
    {
        StartCoroutine("RestartCoroutine");
    }

    IEnumerator RestartCoroutine()
    {
        slider.value = slider.maxValue;
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
