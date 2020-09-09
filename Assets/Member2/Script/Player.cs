using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum PlayerType
{
	User,
	Computer
}

public class Player : MonoBehaviour
{
	MoveRect mMoveRect = null;

	SpriteRenderer mSprite = null;

	protected int playerNum = 0;
	protected int hp = 100;
	protected int sp = 100;

	protected bool curFlip = false;

	public float gapTile = 0.79f;

	private Queue<Card> cardQ;

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
	
	protected virtual void Start()
	{
		cardQ = new Queue<Card>();
		mSprite = GetComponent<SpriteRenderer>();
		mMoveRect = FindObjectOfType<MoveRect>();

		flipPosOn	= new Vector3(-PlayerSize, PlayerSize, 1);
		flipPosOff  = new Vector3(PlayerSize, PlayerSize, 1);

		startPos = transform.position;//new Vector2(-1.65f, 0.26f);
	}
	
	
	public void SetTilePosition(Vector2 newTilePosition)
	{
		m_PrevTilePosition = m_TilePosition;
		m_TilePosition = newTilePosition;

		PlayerManager.Instance.OnPlayerPositionChanged();
	}
	
	
	

	public int GetHP()
	{
		return hp;
	}

	public int GetSP()
	{
		return sp;
	}

	public void UseCard()
    {
		Card card = cardQ.Dequeue();
		if (card is AttackCard) UseAttackCard(card.cardName);
		else if (card is EnergyCard) UseEnergyCard(card.cardName);
		else if (card is GuardCard) UseGuardCard(card.cardName);
		else if (card is HealCard) UseHealCard(card.cardName);
		else if (card is MoveCard) UseMoveCard(card.cardName);
    }
	
	public void UseAttackCard(string name)
    {

    }

	public void UseEnergyCard(string name)
    {

    }

	public void UseGuardCard(string name)
    {

    }

	public void UseHealCard(string name)
    {

    }

	public void UseMoveCard(TBL_MOVE_CARD cardData)
	{
		Vector2 newPosition;
		
		switch (cardData.MoveType)
		{
			case MoveType.Left:
				newPosition = m_TilePosition + new Vector2(0, -cardData.Distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Right:
				newPosition = m_TilePosition + new Vector2(0, cardData.Distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Up:
				newPosition = m_TilePosition + new Vector2(-cardData.Distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Down:
				newPosition = m_TilePosition + new Vector2(cardData.Distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
		}
	}
	
	
	public void UseMoveCard(string name)
    {

    }
}
