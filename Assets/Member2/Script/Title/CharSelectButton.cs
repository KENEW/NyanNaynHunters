using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharSelectButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public CharSelect mCharSelect = null;

	public int CharIndex;   //버튼마다 인덱스 값을 주어서 버튼 이벤트 발생시 반환되는 인덱스 값

	bool enterCheck = false;
	bool downCheck = false;

	void Start()
	{
	}
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
			mCharSelect.GetCharID(CharIndex);
		}
		else
		{
			downCheck = false;
		}
	}
}

