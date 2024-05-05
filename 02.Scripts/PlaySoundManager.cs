using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioListener AL;

    // Update is called once per frame
    void Start()
    {
        GetDefaultLoad();
    }

    public GameObject SoundOnObject;
    public GameObject SoundOffbject;
    public void SoundOff() //Sound¸¦ ²ô¹Ç·Î Sound OffObject°¡ True
    {
        SoundOnObject.SetActive(false);
        SoundOffbject.SetActive(true);
        AudioListener.volume = 0;
        //AL.enabled=false;
        PlayerInfoManager.Instance.SettingSave(1);
    }
    public void SoundOn() 
    {
        SoundOnObject.SetActive(true);
        SoundOffbject.SetActive(false);
        AudioListener.volume = 1f;
        //AL.enabled = true;
        PlayerInfoManager.Instance.SettingSave(0);
    }

    public void GetDefaultLoad() 
    {
        if (PlayerInfoManager.Instance.LoadSettingData() == 1) 
        {
            SoundOff();
        }
        else 
        {
            SoundOn();
        }
    }


}
