using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>,  GameEventListener<GameEvent>
{
    public const int COL = 4;
    public const int ROW = 3;

    public SpriteRenderer m_BackgroundTiles;
    
    public List<Tile> m_Tiles;

    private void Awake()
    {
        this.AddGameEventListening<GameEvent>();
        Hide();
    }

    public void Hide()
    {
        m_BackgroundTiles.enabled = false;
        foreach (var tile in m_Tiles)
        {
            tile.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        m_BackgroundTiles.enabled = true;
        foreach (var tile in m_Tiles)
        {
            tile.gameObject.SetActive(true);
        }
        
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
    
    public bool CanMove(Vector2 tilePosition)
    {
        return 0 <= tilePosition.x && tilePosition.x < ROW && 0 <= tilePosition.y && tilePosition.y < COL;
    }

    public int GetTileIndexByTilePosition(Vector2 tilePosition)
    {
        return (int) tilePosition.x * COL + (int) tilePosition.y;
    }
    
    public Tile GetTile(Vector2 tilePosition)
    {
        if (0 <= tilePosition.x && tilePosition.x < ROW && 0 <= tilePosition.y && tilePosition.y < COL)
        {
            return m_Tiles[(int) tilePosition.x * COL + (int) tilePosition.y];
        }

        return null;
    }
    
    public Vector2 GetLeftPosition(Vector2 tilePosition)
    {
        return m_Tiles[(int)tilePosition.x * COL + (int)tilePosition.y].Left.position;
    }
    
    public Vector2 GetRightPosition(Vector2 tilePosition)
    {
        return m_Tiles[(int)tilePosition.x * COL + (int)tilePosition.y].Right.position;
    }

    public void SetColor(Vector2 tilePosition, float time)
    {
        if (0 <= tilePosition.x && tilePosition.x < ROW && 0 <= tilePosition.y && tilePosition.y < COL)
        {
            m_Tiles[(int)tilePosition.x * COL + (int)tilePosition.y].FlashColor(time);
        }
    }
}
