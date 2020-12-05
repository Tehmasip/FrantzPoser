using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public LayerMask Mask;
    public Vector3 NormalVectorOFPlain;
    public Vector3 StartingPoint;
    public Vector3 NewPosition;
    public Vector3 OriginOfObject;
    public Vector3 StartingRotation;
    public bool IsRotation;

    Vector3 DirToStart;
    Vector3 DirToNew;

    float Angle;

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
                    case 3:
                        AxisVector = Vector3.forward;
                        break;
                }

                OriginOfObject = hit.collider.gameObject.transform.position;
                NormalVectorOFPlain=hit.collider.gameObject.transform.up;
                //D = D * 1000000f;
                //D = Mathf.Round(D);
                //D = D / 100f;
                StartingPoint = hit.point;
                StartingRotation = CurrentObject.transform.rotation.eulerAngles;
                //Debug.Break();
            }
        }
        else if(Input.GetMouseButton(0) && CurrentObject!=null)
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 Dir = ray2.direction.normalized;
            Vector3 PointOnRay = ray2.origin;

            float Denominator = (NormalVectorOFPlain.x * Dir.x) + (NormalVectorOFPlain.y * Dir.y) + (NormalVectorOFPlain.z * Dir.z);

            float Nominator = (NormalVectorOFPlain.x * (StartingPoint.x - PointOnRay.x)) + (NormalVectorOFPlain.y * (StartingPoint.y - PointOnRay.y)) + (NormalVectorOFPlain.z * (StartingPoint.z - PointOnRay.z));


            float T = Nominator / Denominator;

            

            NewPosition = StartingPoint + (T * Dir);
            
            
            

            DirToStart = (StartingPoint - OriginOfObject).normalized;
            DirToNew = (NewPosition - OriginOfObject).normalized;

            


            float DotFromNewToStart = (DirToNew.x * DirToStart.x) + (DirToNew.y * DirToStart.y) + (DirToNew.z * DirToStart.z);
            DotFromNewToStart *= 10000f;
            DotFromNewToStart = Mathf.Round(DotFromNewToStart);
            DotFromNewToStart /= 10000f;


            float Arccos = Mathf.Acos(DotFromNewToStart);



            Angle = Mathf.Acos(DotFromNewToStart)* Mathf.Rad2Deg;
            if(float.IsNaN(Angle))
            {
                Angle = 0f;
            }

            Debug.Log("Angle of Rotation: " + Angle + "   Dot Start : " + DirToStart + "   Dot New : " + DirToNew + "   Angle of Rotation Dor : " + DotFromNewToStart + "   Angle of Rotation arc: " + Arccos);
            //Debug.Log("Angle of Rotation: " + Angle + "\nDir: " + Dir + "\nT : " + T + "\nDenom: " + Denominator + "\n Nominator: " + Nominator);
            CurrentObject.transform.rotation = Quaternion.Euler(StartingRotation);
            CurrentObject.transform.Rotate(AxisVector, Angle, Space.Self);
            //StartingPoint = NewPosition;
            //Debug.Break();

        }
        if(Input.GetMouseButtonUp(0) && CurrentObject!=null)
        {
            CurrentObject = null;
        }
    }
}
