using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointsMotion : MonoBehaviour
{
    public static JointsMotion jointsMotion;

    private void Awake()
    {
        if(jointsMotion == null)
        {
            jointsMotion = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
