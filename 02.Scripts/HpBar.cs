using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject HpMeter;
    public Image HpMeterImage;
    private float duration = 0;
    private void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        CheckHpBar();
    }

    void CheckHpBar() 
    {
        if (HpMeter.activeSelf)
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
            }
            else
            {
                duration = 0;
                HpMeter.SetActive(false);
            }
        }
    }
    public void SetHpBar(float HpRatio)
    {
        //Debug.Log("hpR"+ HpRatio);
        if (HpRatio== 1 || HpRatio == 0)
        {
            duration = 0;
            HpMeter.SetActive(false);
        }
        else
        {
            duration = 3f;
            HpMeter.SetActive(true);
            HpMeterImage.fillAmount = HpRatio;
        }
    }
}
