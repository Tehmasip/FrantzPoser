using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectJoint : MonoBehaviour
{
    private void OnMouseDown()//it is called when ever we click or touch the 
    {
        RotateWithDrag.Instance.SelectedJoint = this.transform;
    }
}
