using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class SelectJoint : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<SphereCollider>().radius = 0.05f;
    }
    private void OnMouseDown()//it is called when ever we click or touch the 
    {
        RotateWithDrag.Instance.SelectedJoint = this.transform;

    }
}
