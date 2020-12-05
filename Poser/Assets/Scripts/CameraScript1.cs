using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript1 : MonoBehaviour
{
    GameObject CurrentActiveTransform = null;
    public LayerMask Mask;

    public float RotationSpeed = 20f;

    public float ScaleChageRate = 20f;

    public bool Draging = false;

    public bool IsRotation;

    

    public float Angle;
    public float ScaleFactor;

    public int Axis;
    public Vector3 AxisVector;
    public GameObject CurrentObject = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(1))
        {
            Debug.Break();
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Mask))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    GameObject Obj = hit.collider.gameObject;
                    int CCount = Obj.transform.childCount;
                    for(int i=0;i<CCount;i++)
                    {
                        GameObject ChildObj = Obj.transform.GetChild(i).gameObject;
                        if (ChildObj.tag== "TransEdit")
                        {
                            if (CurrentActiveTransform != null)
                            {
                                CurrentActiveTransform.SetActive(false);
                                CurrentActiveTransform = null;
                            }

                            ChildObj.SetActive(true);
                            CurrentActiveTransform = ChildObj;
                        }
                    }
                }
                else if (hit.collider.gameObject.layer == 8)
                {
                    CurrentObject = hit.collider.gameObject.GetComponentInChildren<AxisInfo>().Target;
                    Axis = hit.collider.gameObject.GetComponentInChildren<AxisInfo>().Axis;
                    IsRotation = hit.collider.gameObject.GetComponentInChildren<AxisInfo>().Type;


                    switch (Axis)
                    {
                        case 0:
                            AxisVector = Vector3.right;
                            break;
                        case 1:
                            AxisVector = Vector3.up;
                            break;
                        case 2:
                            AxisVector = Vector3.forward;
                            break;
                    }
                }



            }
            else if(CurrentActiveTransform!=null)
            {
                CurrentActiveTransform.SetActive(false);
                CurrentActiveTransform = null;
            }
            
        }
        else if (Input.GetMouseButton(0) && CurrentObject != null)
        {
            float MagDisp = Mathf.Sign(Input.GetAxis("Mouse X")) * Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Mouse X"), 2) + Mathf.Pow(Input.GetAxis("Mouse Y"), 2));
            if (IsRotation)
            {
                Angle = MagDisp * 2f * RotationSpeed * Time.deltaTime;
                CurrentObject.transform.Rotate(AxisVector, -Angle, Space.Self);
            }
            else
            {
                ScaleFactor =1 + (MagDisp * Time.deltaTime * ScaleChageRate);
                CurrentObject.transform.localScale *= ScaleFactor;
            }
        }
        else if (Input.GetMouseButtonUp(0) && CurrentObject != null)
        {
            Draging = false;
            CurrentObject = null;
        }
    }
}
