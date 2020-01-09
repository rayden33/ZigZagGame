using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float destroyTime = 0.3f;

    private bool touched = false;

    void Update()
    {
        if(touched)
        if(destroyTime < 0)
        {
            if(transform.position.y < -1)
            {
                Destroy(transform.gameObject);
            }
            transform.Translate(new Vector3(0, -1, 0).normalized * Time.deltaTime);
        }
        else
        {
            destroyTime -= Time.deltaTime;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            GlobalData.touchedPlatforms++;
            touched = true;
        }
    }
}
