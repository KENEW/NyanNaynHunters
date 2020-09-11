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

        this.gameObject.SetActive(false);
        
    }
    
    public void Init()
    {
        CurrentScene = 0;
    }

    public void SetBackground()
    {
        m_SpriteRenderer.sprite = m_Backgrounds[CurrentScene];
    }

    public void OnGameEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case GameEventType.StageStart:
                gameObject.SetActive(true);
                break;
        }
    }
}
