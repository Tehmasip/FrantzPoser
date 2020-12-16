using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisInfo : MonoBehaviour
{
    public bool Type;
    private bool IsParentChanged = false;
    public int Axis;
    public GameObject Target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ParentChanged(bool Che)
    {
        IsParentChanged = Che;
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsParentChanged)
        {
            //transform.localScale = Vector3.one;
        }
    }
}
