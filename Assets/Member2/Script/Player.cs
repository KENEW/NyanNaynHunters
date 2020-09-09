using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

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
	
	protected virtual void Start()
	{
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


	public void UseCard(Card card)
    {
		if (card is AttackCard) UseAttackCard(((AttackCard)card));
		else if (card is EnergyCard) UseEnergyCard(((EnergyCard)card));
		else if (card is GuardCard) UseGuardCard(((GuardCard)card));
		else if (card is HealCard) UseHealCard(((HealCard)card));
		else if (card is MoveCard) UseMoveCard(((MoveCard)card));
    }
	
	public void UseAttackCard(AttackCard attackCard)
	{
		int index = TBL_ATTACK_CARD.CountEntities;
		while (--index >= 0)
		{
			if (TBL_ATTACK_CARD.GetEntity(index).name.Equals(attackCard.cardName)) break;
		}

		AttackCard card;
		if (index >= 0)
		{
			card = new AttackCard(TBL_ATTACK_CARD.GetEntity(index));
			for (int i = 0; i < card.positions.Count; i++)
			{
				Vector2 me = new Vector2(mMoveRect.playerPos[playerNum, 0], mMoveRect.playerPos[playerNum, 1]);
				Vector2 enemyPos = new Vector2(mMoveRect.playerPos[playerNum == 0 ? 1 : 0, 0], mMoveRect.playerPos[playerNum == 0 ? 1 : 0, 1]);
				Vector2 v = GetPositionOfNumber(card.positions[i]);
				Vector2 pos = me + v;

                if (pos == enemyPos)
                {
					target.AddHP(-card.damage);
					AddSP(-card.energyCost);
                }
			}
		}
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

	private Vector2 GetPositionOfNumber(int n)
	{
		Vector2 v;
		if (n == 0)		 v = new Vector2(-1f, 1f);
		else if (n == 1) v = new Vector2(0f, 1f);
		else if (n == 2) v = new Vector2(1f, 1f);
		else if (n == 3) v = new Vector2(-1f, 0f);
		else if (n == 4) v = new Vector2(0f, 0f);
		else if (n == 5) v = new Vector2(1f, 0f);
		else if (n == 6) v = new Vector2(-1f, -1f);
		else if (n == 7) v = new Vector2(0f, -1f);
		else if (n == 8) v = new Vector2(1f, -1f);
		else v = new Vector2(2f, 2f);
		return v;
	}

	public void UseEnergyCard(EnergyCard energyCard)
    {

    }

	public void UseGuardCard(GuardCard guardCard)
    {

    }

	public void UseHealCard(HealCard healCard)
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
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Right:
				newPosition = m_TilePosition + new Vector2(0, cardData.distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Up:
				newPosition = m_TilePosition + new Vector2(-cardData.distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Down:
				newPosition = m_TilePosition + new Vector2(cardData.distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
		}
	}
}
