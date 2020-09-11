using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>, GameEventListener<GameEvent>
{
    public SpriteRenderer m_SpriteRenderer;
    
    public List<Sprite> m_Backgrounds;

    public int CurrentScene = 0;


    private void Awake()
    {
        this.AddGameEventListening<GameEvent>();
        Hide();
    }
    
    public void Init()
    {
        CurrentScene = 0;
    }

    public void SetBackground()
    {
        m_SpriteRenderer.sprite = m_Backgrounds[CurrentScene];
    }

    public void Hide()
    {
        m_SpriteRenderer.enabled = false;
    }

    public void Show()
    {
        m_SpriteRenderer.enabled = true;
    }
    public void OnGameEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case GameEventType.StageStart:
                Show();
                break;
        }
    }
}
