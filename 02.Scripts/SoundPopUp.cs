using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SoundsPannel;

    public void SoundsPannelToggle()
    {
        SoundsPannel.SetActive(!SoundsPannel.activeSelf);
    }
}
