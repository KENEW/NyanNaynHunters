using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public PanelManage mPanelManage = null;
	public void OnPointerDown(PointerEventData data)
	{
		
	}

	public void OnPointerUp(PointerEventData data)
	{
		mPanelManage.NextScene();
	}
}
