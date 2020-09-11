using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCanvas : MonoSingleton<TitleCanvas>, GameEventListener<GameEvent>
{
    private CanvasGroup m_CanvasGroup;
    
    public void Awake()
    {
        m_CanvasGroup = GetComponent<CanvasGroup>();
        this.AddGameEventListening<GameEvent>();
    }

    public void OnGameEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case GameEventType.StageStart:
                m_CanvasGroup.alpha = 0;
                m_CanvasGroup.blocksRaycasts = false;
                break;
        }
    }
}
