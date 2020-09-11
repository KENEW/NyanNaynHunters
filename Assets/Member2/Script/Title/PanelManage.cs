using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManage : MonoBehaviour
{
    public CharSelect mCharSelect = null;

    public GameObject TitlePanel = null;
    public GameObject StoryPanel = null;
    public GameObject SelectPanel = null;
    public GameObject CreditPanel = null;

    public Image FadePanel = null;
    public Animator mAnimator = null;
    public bool isFade = false;

    int curCount = 0;

    [SerializeField] float fadeSpeed = 100.0f;
    Color color;
    bool isPlayCheck = false;
    private void Start()
    {
        SoundManager.Instance.PlayBGM("Main_BGM1");
    }

    private void Update()
	{
        if(FadePanel.color.a >= 0.9f)
        {
            if(!isPlayCheck)
            {
                isPlayCheck = true;
                PanelCount();
            }
        }
        if (FadePanel.color.a <= 0.05f && mCharSelect.curCount == 0)
        {
            FadePanel.raycastTarget = false;
        }
        else
        {
            FadePanel.raycastTarget = true;
        }
    }
    void PanelCount()
    {
		switch (curCount)
		{
            case 1:
                StoryPanel.SetActive(true);
                TitlePanel.SetActive(false);
                break;
            case 2:
                StoryPanel.SetActive(false);
                SelectPanel.SetActive(true);
                SoundManager.Instance.StopBGM();
                SoundManager.Instance.PlayBGM("Character_Select_BGM");
                break;
        }
            
	}
    public void NextScene()
    {
        Debug.Log("디버그");
        isPlayCheck = false;
            curCount++;
        mAnimator.SetTrigger("Fade");
    }
    bool isCredit = false;
    public void CreditPanelButton()
    {
        Debug.Log("버튼");
        if(!isCredit)
        {
            isCredit = true;
            CreditPanel.SetActive(isCredit);
        }
        if (isCredit)
        {
            isCredit = false;
            CreditPanel.SetActive(isCredit);
        }
    }
}
