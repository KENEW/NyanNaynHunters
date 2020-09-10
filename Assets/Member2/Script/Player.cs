using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;

public enum PlayerType
{
	User,
	Computer
}

public class Player : MonoBehaviour
{
	MoveRect mMoveRect = null;

	protected int playerNum = 0;
	protected int hp = 100;
	protected int sp = 100;

	protected bool curFlip = false;

	public float gapTile = 0.79f;

	public Player target;

	[SerializeField]
	float PlayerSize = 0.4f;

	Vector2 startPos = Vector2.zero;

	Vector3 flipPosOn	= Vector3.zero;
	Vector3 flipPosOff	= Vector3.zero;

	private Vector2 m_PrevTilePosition;
	public Vector2 prevTilePosition => m_PrevTilePosition;
	
	private Vector2 m_TilePosition;
	public Vector2 tilePosition => m_TilePosition;

	public PlayerType playerType;

	public Slider hpSlider;
	public Slider spSlider;
	
	public const float LeftXScale = -1;
	public const float RightXScale = 1;

	[Header("스켈레톤 에니메이션")] public SkeletonAnimation m_SkeletonAnimation;
	
	protected virtual void Awake()
    {
		hp = TBL_GAME_SETTING.GetEntity(0).PlayerStartHP;
		sp = TBL_GAME_SETTING.GetEntity(0).PlayerStartEnergy;

		hpSlider.maxValue = hp;
		hpSlider.value = hp;
		spSlider.maxValue = sp;
		spSlider.value = sp;

	}

	protected virtual void Start()
	{
		mMoveRect = FindObjectOfType<MoveRect>();

		flipPosOn	= new Vector3(-PlayerSize, PlayerSize, 1);
		flipPosOff  = new Vector3(PlayerSize, PlayerSize, 1);

		startPos = transform.position;//new Vector2(-1.65f, 0.26f);
	}

    private void Update()
    {
		Debug.Log(name + " " + m_TilePosition);
    }


    public void SetTilePosition(Vector2 newTilePosition, bool xMove)
	{
		m_PrevTilePosition = m_TilePosition;
		m_TilePosition = newTilePosition;

		PlayerManager.Instance.OnPlayerPositionChanged(xMove, playerType);
	}


	public void LeftLerpMove(Vector3 dist, float moveSpeed, bool xMove)
	{
		transform.localScale = new Vector3(LeftXScale, 1, 1);

		if (Vector3.Distance(dist, transform.position) < 0.1) return;
		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Move", true);
		transform.DOJump(dist, moveSpeed, 1, 0.5f).OnComplete(() =>
		{
			m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
		});
	}
	
	public void RightLerpMove(Vector3 dist, float moveSpeed, bool xMove)
	{
		transform.localScale = new Vector3(RightXScale, 1, 1);

		if (Vector3.Distance(dist, transform.position) < 0.1) return;
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Move", true);
		transform.DOJump(dist, moveSpeed, 1, 0.5f).OnComplete(() =>
		{
			m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
		});
	}

	public int GetHP()
	{
		return hp;
	}

	public int GetSP()
	{
		return sp;
	}


	public void UseCard(Card card)
    {
		if (card is AttackCard) UseAttackCard(((AttackCard)card));
		else if (card is EnergyCard) UseEnergyCard(card.cardName);
		else if (card is GuardCard) UseGuardCard(card.cardName);
		else if (card is HealCard) UseHealCard(card.cardName);
		else if (card is MoveCard) UseMoveCard(((MoveCard)card));
		Debug.Log(name+" Use card");
    }
	
	private void UseAttackCard(AttackCard card)
	{

		Player target;
		if (playerType == PlayerType.User)
			target = PlayerManager.Instance.Enemy;
		else
			target = PlayerManager.Instance.Player;

        if (InTarget(card.positions, target.tilePosition)) //명중하면 true, 빗나가면 false
        {
			target.AddHP(-card.damage);
        }

		AddSP(-card.energyCost);
	}

	private bool InTarget(List<int> list, Vector2 targetPosition)
    {
		bool inp = false;

		for (int i=0; i<list.Count; i++)
        {
			Vector2 tilePosition = GetPosition(list[i]);
			if(tilePosition == targetPosition) //스킬범위 안
            {
				inp = true;
            }
        }
		return inp;
    }

	private Vector2 GetPosition(int pos)
    {
		// 스테이지: N(x, y)
		//  0(0,0)   1(0, 1)   2(0, 2)    3(0, 3)
		//  4(1,0)   5(1, 1)   6(1, 2)    7(1, 3)
		//  8(2,0)   9(2, 1)  10(2, 2)   11(2, 3)
		// 12(3,0)  13(3, 1)  14(3, 2)   15(3, 3)


		// 타일 범위
		// 0  1  2
		// 3  4  5
		// 6  7  8

		var playerTilePosition = m_TilePosition;

		Vector2 selectedTile = Vector2.zero;
		switch (pos)
		{
			case 0:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y - 1;
				break;

			case 1:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y - 1;
				break;
			case 2:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y - 1;
				break;
			case 3:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y;
				break;

			case 4:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y;
				break;
			case 5:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y;
				break;
			case 6:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y + 1;
				break;

			case 7:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y + 1;
				break;
			case 8:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y + 1;
				break;

		}

		return selectedTile;
	}

	private void AddHP(int value)
    {
		hp += value;
		hpSlider.value = hp;
    }

	private void AddSP(int value)
    {
		sp += value;
		spSlider.value = sp;
    }


	private void UseEnergyCard(string name)
    {

    }

	private void UseGuardCard(string name)
    {

    }

	private void UseHealCard(string name)
    {

    }

	public void UseMoveCard(MoveCard cardData)
	{
		Vector2 newPosition;
		
		switch (cardData.moveType)
		{
			case MoveType.Left:
				newPosition = m_TilePosition + new Vector2(0, -cardData.distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition, true);
				}
				break;
			
			case MoveType.Right:
				newPosition = m_TilePosition + new Vector2(0, cardData.distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition, true);
				}
				break;
			
			case MoveType.Up:
				newPosition = m_TilePosition + new Vector2(-cardData.distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition, false);
				}
				break;
			
			case MoveType.Down:
				newPosition = m_TilePosition + new Vector2(cardData.distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition, false);
				}
				break;
		}
	}
}
