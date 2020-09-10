using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharSelect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public int CharIndex = 0;

	bool enterCheck = false;
	bool downCheck = false;

	public void OnPointerDown(PointerEventData data)
	{
		downCheck = true;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		enterCheck = true;
	}
	public void OnPointerExit(PointerEventData data)
	{
		enterCheck = false;
	}

	public void OnPointerUp(PointerEventData data)
	{
		if (enterCheck && downCheck)
		{
			//함수 전달


			Debug.Log(CharIndex);
		}
		else
		{
			downCheck = false;
		}
	}
}
