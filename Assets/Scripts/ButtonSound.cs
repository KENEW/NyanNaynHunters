using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public string sfxName;

    public void OnSound()
    {
        SoundManager.Instance.PlaySFX(sfxName);
    }
}
