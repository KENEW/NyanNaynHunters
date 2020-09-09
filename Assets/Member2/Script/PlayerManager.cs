using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private readonly Vector2 PlayerStartPosition = new Vector2(1, 0);
    private readonly Vector2 EnemeyStartPosition = new Vector2(1, 2);
    
    public Player Player;
    public Player Enemy;


    private void Start()
    {
        InitPosition();
    }

    public void InitPosition()
    {
        Player.SetTilePosition(PlayerStartPosition);
        Enemy.SetTilePosition(EnemeyStartPosition);
    }

    public void OnPlayerPositionChanged()
    {
        var playerTilePosition = Player.tilePosition;
    
        var enemyTilePosition = Enemy.tilePosition;

        // 플레이어가 왼쪾인 경우
        if (playerTilePosition.y < enemyTilePosition.y)
        {
            Player.transform.position = TileManager.Instance.GetLeftPosition(playerTilePosition);
            Player.transform.localScale = new Vector3(-1, 1, 1);
            Enemy.transform.position = TileManager.Instance.GetRightPosition(enemyTilePosition);
            Enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        // 위치가 같은 경우
        else if (playerTilePosition.y == enemyTilePosition.y)
        {
            if (Player.transform.localScale.x == -1)
            {
                Player.transform.position = TileManager.Instance.GetLeftPosition(playerTilePosition);
                Player.transform.localScale = new Vector3(-1, 1, 1);
                Enemy.transform.position = TileManager.Instance.GetRightPosition(enemyTilePosition);
                Enemy.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Player.transform.position = TileManager.Instance.GetRightPosition(playerTilePosition);
                Player.transform.localScale = new Vector3(1, 1, 1);
                Enemy.transform.position  = TileManager.Instance.GetLeftPosition(enemyTilePosition);
                Enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        // 플레이어가 오른쪽인 경우
        else if (playerTilePosition.y > enemyTilePosition.y)
        {
            Player.transform.position = TileManager.Instance.GetRightPosition(playerTilePosition);
            Player.transform.localScale = new Vector3(1, 1, 1);
            Enemy.transform.position  = TileManager.Instance.GetLeftPosition(enemyTilePosition);
            Enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
        


    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Player.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(0)));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Player.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(1)));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Player.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(2)));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Player.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(3)));
        }
    }
}