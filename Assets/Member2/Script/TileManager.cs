using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    private const int COL = 4;
    private const int ROW = 3;
    
    public List<Tile> m_Tiles;

    public bool CanMove(Vector2 tilePosition)
    {
        return 0 <= tilePosition.x && tilePosition.x < ROW && 0 <= tilePosition.y && tilePosition.y < COL;
    }
    
    public Vector2 GetPosition(Vector2 tilePosition)
    {
        return m_Tiles[(int)tilePosition.x * COL + (int)tilePosition.y].Left.position;
    }
    
    public Vector2 GetLeftPosition(Vector2 tilePosition)
    {
        return m_Tiles[(int)tilePosition.x * COL + (int)tilePosition.y].Left.position;
    }
    
    public Vector2 GetRightPosition(Vector2 tilePosition)
    {
        return m_Tiles[(int)tilePosition.x * COL + (int)tilePosition.y].Right.position;
    }
}
