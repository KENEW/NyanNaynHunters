using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerProfile : MonoBehaviour, GameEventListener<GameEvent>
{
    public List<Sprite> m_Sprites;
    
    public bool isPlayer = true; 
    public Image m_Image;
    
    
    private void Awake()
    {
        m_Image = GetComponent<Image>();
        
        this.AddGameEventListening<GameEvent>();
    }


    private void OnDisable()
    {
        this.RemoveGameEventListening<GameEvent>();
    }
    
    public void OnGameEvent(GameEvent e)
    {
        if (e.Type == GameEventType.GameStart)
        {
            int index;
            index = isPlayer ? PlayerManager.Instance.Player.Index : PlayerManager.Instance.Enemy.Index;

            m_Image.sprite = m_Sprites[index];
        }
    }
}
