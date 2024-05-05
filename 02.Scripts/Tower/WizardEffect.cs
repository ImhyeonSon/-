using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 targetV;
    float timeF = 0.4f;
    void Start()
    {
        
    }
    bool isMove = false;
    void OnEnable()
    {
        timeF = 0.4f;
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        EffectDestroy();
        if (isMove)
        {
            EffectMove();
        }
    }
    public void EffectSetting(Vector3 monsterV, Vector3 startV)
    {
        targetV = monsterV;
        transform.position = startV;
        isMove = true;
    }
    void EffectMove()
    {
        Vector3 dir = targetV- transform.position;
        transform.Translate(dir.normalized * 40f * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, targetV) <= 0.2f)
        {
            isMove=false;
        }
    }
    void EffectDestroy()
    {
        if (timeF > 0)
        {
            timeF -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

