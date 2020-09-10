using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharSelect : MonoBehaviour
{
	public int[] charID = new int[2] { 0, 0 };	//전달을 위해 저장되는 인덱스 값 / 0 = player, 1 = computer
	public int curCount = 0;					//현재 배열에 접근하기 위한 현재 카운트 수
}
