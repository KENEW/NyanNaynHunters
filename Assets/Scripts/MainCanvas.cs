using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainCanvas : MonoSingleton<MainCanvas>, GameEventListener<GameEvent>
{

    public CanvasGroup m_CanvasGroup;
    
    private void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        m_CanvasGroup.alpha = 0;
        
        this.AddGameEventListening<GameEvent>();
    }


    public void OnGameEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case GameEventType.StageStart:
                m_CanvasGroup.alpha = 1;
                break;
        }
    }
}
