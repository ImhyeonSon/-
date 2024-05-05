using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    float timeF = 0.3f;
    private void OnEnable()
    {
        timeF = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        EffectDestroy();
    }
    public void EffectSetting(Vector3 monsterV, float damage, MonsterBehavior[] monsters)
    {
        transform.position = monsterV+3*Vector3.up;
        foreach (MonsterBehavior MB in monsters)
        { //안 죽었을 때, 사거리에 들어왔다면
            if (!MB.GetIsDie() && Vector3.Distance(MB.transform.position, transform.position) < 6f)
            {
                MB.SetDamage(damage);
            }
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
