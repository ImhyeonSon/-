using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // Start is called before the first frame update
    float life = 1.5f;
    void Start()
    {
        life = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.position += 3*Vector3.up*Time.deltaTime; 
        life-=Time.deltaTime;
        if (life <= 0) 
        {
            Destroy(gameObject);
        }
    }


}
