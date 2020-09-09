using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private readonly Vector2 PlayerStartPosition = new Vector2(1, 0);
    private readonly Vector2 EnemeyStartPosition = new Vector2(1, 2);

    public Player Player;
    public Player Enemy;

    [SerializeField] private float moveSpeed = 1.1f;
   
    private void PlayerLerpMove(Vector3 dist, float time, bool xMove)
    {
        if(xMove)
        {
            Player.transform.DOMoveX(dist.x, time);
        }
        else
        {
            Player.transform.DOJump(dist, moveSpeed, 1, 0.3f);
        }
    }
    private void EnemyLerpMove(Vector3 dist, float time, bool xMove)
    {
        if (xMove)
        {
            Enemy.transform.DOMoveX(dist.x, time);
        }
        else
        {
            Enemy.transform.DOJump(dist, moveSpeed, 1, 0.3f);
        }
    }
    private void Start()
    {
        InitPosition();
    }

    public void InitPosition()
    {
        Player.SetTilePosition(PlayerStartPosition, false, 0);
        Enemy.SetTilePosition(EnemeyStartPosition, false, 1);
    }

    public void OnPlayerPositionChanged(bool xMove, int checkPlayer)
    {
        var playerTilePosition = Player.tilePosition;
        var enemyTilePosition = Enemy.tilePosition;

        // 플레이어가 왼쪾인 경우
        if (playerTilePosition.y < enemyTilePosition.y)
        {
            if(checkPlayer == 0) PlayerLerpMove(TileManager.Instance.GetLeftPosition(playerTilePosition), moveSpeed, xMove);
            if(checkPlayer == 1) EnemyLerpMove(TileManager.Instance.GetRightPosition(enemyTilePosition), moveSpeed, xMove);

            Player.transform.localScale = new Vector3(-1, 1, 1);
            Enemy.transform.localScale = new Vector3(1, 1, 1);
        }
        // 위치가 같은 경우
        else if (playerTilePosition.y == enemyTilePosition.y)
        {
            if (Player.transform.localScale.x == -1)
            {
                if (checkPlayer == 0)   PlayerLerpMove(TileManager.Instance.GetLeftPosition(playerTilePosition), moveSpeed, xMove);
                if (checkPlayer == 1)   EnemyLerpMove(TileManager.Instance.GetRightPosition(enemyTilePosition), moveSpeed, xMove);
                        
                Player.transform.localScale = new Vector3(-1, 1, 1);
                Enemy.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (checkPlayer == 0)   PlayerLerpMove(TileManager.Instance.GetLeftPosition(playerTilePosition), moveSpeed, xMove);
                if (checkPlayer == 1)   EnemyLerpMove(TileManager.Instance.GetLeftPosition(enemyTilePosition), moveSpeed, xMove);
                        
                Player.transform.localScale = new Vector3(1, 1, 1);
                Enemy.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        // 플레이어가 오른쪽인 경우
        else if (playerTilePosition.y > enemyTilePosition.y)
        {
            if (checkPlayer == 0)   PlayerLerpMove(TileManager.Instance.GetLeftPosition(playerTilePosition), moveSpeed, xMove);
            if (checkPlayer == 1)   EnemyLerpMove(TileManager.Instance.GetLeftPosition(enemyTilePosition), moveSpeed, xMove);
                    
            Player.transform.localScale = new Vector3(1, 1, 1);
            Enemy.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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

        //적 플레이어 디버그
        if (Input.GetKeyDown(KeyCode.A))
        {
            Enemy.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(0)));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Enemy.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(1)));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Enemy.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(2)));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Enemy.UseMoveCard(new MoveCard(TBL_MOVE_CARD.GetEntity(3)));
        }
    }
}