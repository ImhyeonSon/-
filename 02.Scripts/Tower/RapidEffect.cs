using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RapidEffect : MonoBehaviour
{
    // Start is called before the first frame update
    float timeF = 0.05f;
    private void OnEnable()
    {
        timeF = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        EffectDestroy();
    }
    public void EffectSetting(Vector3 monsterV)
    {
        transform.position = monsterV;
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
