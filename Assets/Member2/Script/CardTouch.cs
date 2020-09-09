using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardTouch : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
	public CardField mCardFiled;

	void Start()
	{
		mCardFiled = FindObjectOfType<CardField>();
	}
	public void OnPointerDown(PointerEventData ped)
	{
		
	}

	public void OnPointerUp(PointerEventData ped)
	{
		Debug.Log("클릭");
	}
}
