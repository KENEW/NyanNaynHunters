using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoSingleton<SliderManager>
{
    public Slider PlayerHPSlider;
    public Slider PlayerSPSlider;
    public Slider EnemyHPSlider;
    public Slider EnemySPSlider;
    
    private void Awake()
    {
        PlayerHPSlider = GameObject.FindGameObjectWithTag("PlayerHPBar").GetComponent<Slider>();
        PlayerSPSlider = GameObject.FindGameObjectWithTag("PlayerSPBar").GetComponent<Slider>();
        EnemyHPSlider  = GameObject.FindGameObjectWithTag("EnemyHPBar").GetComponent<Slider>();
        EnemySPSlider  = GameObject.FindGameObjectWithTag("EnemySPBar").GetComponent<Slider>();
    }
}
