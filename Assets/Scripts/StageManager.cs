using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public SpriteRenderer m_SpriteRenderer;
    
    public List<Sprite> m_Backgrounds;

    public int CurrentScene = 0;


    public void Init()
    {
        CurrentScene = 0;
    }

    public void SetBackground()
    {
        m_SpriteRenderer.sprite = m_Backgrounds[CurrentScene];
    }
}
