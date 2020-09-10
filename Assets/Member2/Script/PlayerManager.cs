using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private readonly Vector2 PlayerStartPosition = new Vector2(1, 0);
    private readonly Vector2 EnemeyStartPosition = new Vector2(1, 2);


    public List<Player> m_Playsers;
    private List<Player> m_UsedPlayers = new List<Player>(4);
    
    public Player Player;
    public Player Enemy;

    [SerializeField] private float moveSpeed = 1.1f;
    
    private void Start()   // 요건 나중에 없앨 예정 
    {

        SelectPlayerCharacter(0);
        SelectRandomEnemyCharacter();
        SetShowAndHide();
        InitPosition();
    }

    public void SelectPlayerCharacter(int index) // 0 ~ 3
    {
        Player = m_Playsers[index];
        m_UsedPlayers.Add(Player);
    }

    public void SelectRandomEnemyCharacter()
    {
        if (m_UsedPlayers.Count >= m_Playsers.Count) return;
        
        while (true)
        {
            int randomIndex = Random.Range(0, m_Playsers.Count);

            if (m_UsedPlayers.Contains(m_Playsers[randomIndex]))
            {
                continue;
            }

            Enemy = m_Playsers[randomIndex];
            m_UsedPlayers.Add(Enemy);
            break;
        }
    }

    public void SetShowAndHide()
    {
        foreach (var player in m_Playsers)
        {
            player.gameObject.SetActive(player == Player || player == Enemy);
        }
    }

    public void InitPosition()
    {
        Player.SetTilePosition(PlayerStartPosition, false);
        Enemy.SetTilePosition(EnemeyStartPosition, false);
    }

    public void OnPlayerPositionChanged(bool xMove, PlayerType playerType)
    {
        var playerTilePosition = Player.tilePosition;
        var enemyTilePosition = Enemy.tilePosition;

       
        // 플레이어가 왼쪾인 경우
        if (playerTilePosition.y < enemyTilePosition.y)
        {
            Player.LeftLerpMove(TileManager.Instance.GetLeftPosition(playerTilePosition), moveSpeed, xMove);
            Enemy.RightLerpMove(TileManager.Instance.GetRightPosition(enemyTilePosition), moveSpeed, xMove);
        }
        // 위치가 같은 경우
        else if (playerTilePosition.y == enemyTilePosition.y)
        {
            if (Player.transform.localScale.x == -1)
            {
                Player.LeftLerpMove(TileManager.Instance.GetLeftPosition(playerTilePosition), moveSpeed, xMove);
                Enemy.RightLerpMove(TileManager.Instance.GetRightPosition(enemyTilePosition), moveSpeed, xMove);
            }
            else
            {
                Player.RightLerpMove(TileManager.Instance.GetRightPosition(playerTilePosition), moveSpeed, xMove);
                Enemy.LeftLerpMove(TileManager.Instance.GetLeftPosition(enemyTilePosition), moveSpeed, xMove);
            }
        }
        // 플레이어가 오른쪽인 경우
        else if (playerTilePosition.y > enemyTilePosition.y)
        {
            Player.RightLerpMove(TileManager.Instance.GetRightPosition(playerTilePosition), moveSpeed, xMove);
            Enemy.LeftLerpMove(TileManager.Instance.GetLeftPosition(enemyTilePosition), moveSpeed, xMove);
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