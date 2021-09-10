using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript2 : MonoBehaviour
{
    [Header("Camera Controls Variables")]
    public GameObject CurrentCameraTarget=null;
    public float MinimumOutZoom = 20;
    public float MaximumOutZoom = 80f;
    public GameObject CurrentTargetObject = null;
    public float PaningSpeed = 3f;
    public float CameraRotationSpeed = 10f;
    Vector3 CameraRotationDirection;
    public float CameraZoomSpeed = 3f;
    public bool CameraRotating = false;
    public bool CameraPaning = false;
    public float ScrollScaleFactor = 0.1f;
    private bool IsCameraBiengControles=false;

    public Vector3 CameraRotatingMouseFirstPosition;
    public Vector3 CameraRotatingMouseSecondPosition;

    [Header("Editiing Variables")]
    GameObject CurrentActiveTransform = null;
    public LayerMask Mask;

    public float RotationSpeed = 20f;

    public float ScaleChageRate = 20f;

    public bool Draging = false;

    public bool IsRotation;

    public Vector3 MouseFirstPosition;
    public Vector3 MouseSecondPosition;
    public Vector3 MouseMovementDirection;
    public Vector3 ProjectionOfMouseDirection;
    public Vector3 CurrentAxisNormal;

    public float MouseDirectionMagnitude;

    public GameObject CurrentAxisObject;

    public Vector3 MouseOriginalHitPoint;
    public Vector3 MouseOriginalHitPointInLocalToAxis;
    public Vector3 VectorFromOriginToMouseHitPoint;
    public int AxisToCheckProjectionWith;
    public Vector3 AxisToProjectVectorOn;

    public float DirectionOfRotation = 1f;

    public float Angle;
    public float ScaleFactor;

    public int Axis;
    public Vector3 AxisVector;
    public GameObject CurrentObject = null;
    public GameObject AxisParent = null;
    public Transform AxisTransformDataDuplicate = null;
    public Vector3 CurrentTangentVectorInLocalSpace;
    // Start is called before the first frame update
    void Start()
    {
        if(CurrentTargetObject!=null)
        {
            Camera.main.transform.LookAt(CurrentTargetObject.transform);
            
        }
        
    }

    public void SetCurrentTargetObject(GameObject Obj)
    {
        CurrentCameraTarget = Obj;
        Camera.main.transform.LookAt(CurrentCameraTarget.transform);

    }

    public void FlipCamControls()
    {
        IsCameraBiengControles = !IsCameraBiengControles;
        if (CurrentActiveTransform != null)
        {
            CurrentActiveTransform.SetActive(!IsCameraBiengControles);
        }
    }
    
    private void CameraRotatingAround(Vector3 Direction)
    {
        if(CurrentCameraTarget==null)
        {
            
            Camera.main.transform.Rotate(new Vector3(1,0,0) , -Direction.y * 180f);
            Camera.main.transform.Rotate(new Vector3(0,1,0) , Direction.x * 180f,Space.World);
        }
        else
        {
            float ObjZDist = (Camera.main.transform.position - CurrentCameraTarget.transform.position).magnitude;
            Camera.main.transform.position = CurrentCameraTarget.transform.position;

            Camera.main.transform.Rotate(new Vector3(1, 0, 0), -Direction.y * 180f);
            Camera.main.transform.Rotate(new Vector3(0, 1, 0), Direction.x * 180f, Space.World);
            Camera.main.transform.Translate(new Vector3(0, 0, -ObjZDist));

        }
    }

   

    private void ZoomValue(float incremeant)
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - incremeant, MinimumOutZoom, MaximumOutZoom);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCameraBiengControles)
        {
            if ((Input.GetMouseButtonDown(0)) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                CameraRotating = true;

                if(Input.touchCount == 1)
                {
                    CameraRotatingMouseFirstPosition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                }
                else
                {
                    CameraRotatingMouseFirstPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                }
                //rotation

            }
            if ((Input.GetMouseButton(0) || (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))) && CameraRotating)
            {
                //Vector3 MouseMovement = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0f);
                CameraRotatingMouseSecondPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                CameraRotationDirection = CameraRotatingMouseFirstPosition - CameraRotatingMouseSecondPosition;
                CameraRotatingAround(CameraRotationDirection);
                CameraRotatingMouseFirstPosition = CameraRotatingMouseSecondPosition;
            }
            if ((Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)) && CameraRotating)
            {
                CameraRotating = false;
            }

            if ((Input.GetMouseButtonDown(1)) || (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)))
            {
                CameraPaning = true;
                //rotation

            }
            if ((Input.GetMouseButton(1) || (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(1).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Stationary))) && CameraPaning)
            {
                Vector3 MouseMovement = new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0f);
                Camera.main.transform.Translate(MouseMovement * Time.deltaTime * PaningSpeed, Space.Self);
            }
            if ((Input.GetMouseButtonUp(1) || (Input.touchCount == 2 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended))) && CameraPaning)
            {
                CameraPaning = false;
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrivious = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrivious = touchOne.position - touchOne.deltaPosition;

                float CurrentMagnitude = (touchZero.position - touchOne.position).magnitude;
                float PrivousMagnitude = (touchZeroPrivious - touchOnePrivious).magnitude;

                float Difference = CurrentMagnitude - PrivousMagnitude;
                ZoomValue(Difference * CameraZoomSpeed);

            }
            ZoomValue(Input.GetAxis("Mouse ScrollWheel") * CameraZoomSpeed);




        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Mask))
                {
                    if (hit.collider.gameObject.layer == 9)
                    {
                        GameObject Obj = hit.collider.gameObject;
                        int CCount = Obj.transform.childCount;
                        for (int i = 0; i < CCount; i++)
                        {
                            GameObject ChildObj = Obj.transform.GetChild(i).gameObject;
                            if (ChildObj.tag == "TransEdit")
                            {
                                if (CurrentActiveTransform != null)
                                {
                                    CurrentActiveTransform.SetActive(false);
                                    CurrentActiveTransform = null;
                                    CurrentTargetObject = null;
                                }

                                ChildObj.SetActive(true);
                                CurrentActiveTransform = ChildObj;
                                CurrentTargetObject = Obj;
                                //SetCurrentTargetObject(Obj);
                            }
                        }
                    }
                    else if (hit.collider.gameObject.layer == 8)
                    {
                        IsRotation = hit.collider.gameObject.GetComponentInChildren<AxisInfo>().Type;
                        //get axis
                        CurrentAxisObject = hit.collider.gameObject;
                        //get axis parent
                        AxisParent = CurrentAxisObject.transform.parent.gameObject;

                        //get target object
                        CurrentObject = hit.collider.gameObject.GetComponentInChildren<AxisInfo>().Target;

                        //get target object parent as axis parent
                        Transform CurrentObjectParent = CurrentObject.transform.parent;
                        CurrentAxisObject.transform.parent = CurrentObjectParent;
                        hit.collider.gameObject.GetComponentInChildren<AxisInfo>().ParentChanged(true);
                        

                        //set axis as target object parent
                        CurrentObject.transform.parent = CurrentAxisObject.transform;


                        AxisTransformDataDuplicate = (new GameObject()).transform;
                        AxisTransformDataDuplicate.SetPositionAndRotation(CurrentAxisObject.transform.position, CurrentAxisObject.transform.rotation);


                        Axis = hit.collider.gameObject.GetComponentInChildren<AxisInfo>().Axis;
                        CurrentAxisNormal = CurrentAxisObject.transform.up;

                        



                        Vector3 MP = Input.mousePosition;
                        MP.z = 0.3f;
                        MouseFirstPosition = Camera.main.ScreenToWorldPoint(MP);

                        //get hit point
                        MouseOriginalHitPoint = hit.point;
                        
                        //convert hit point to local space
                        MouseOriginalHitPointInLocalToAxis = AxisTransformDataDuplicate.InverseTransformPoint(MouseOriginalHitPoint);
                        MouseOriginalHitPointInLocalToAxis.y = 0;

                        //get tangent to circle with center at transform.position and radius distance of point in local space with positive value is clockwise rotation
                        CurrentTangentVectorInLocalSpace = new Vector3(MouseOriginalHitPointInLocalToAxis.z, 0, -MouseOriginalHitPointInLocalToAxis.x);
                    }



                }
                else if (CurrentActiveTransform != null)
                {
                    CurrentActiveTransform.SetActive(false);
                    CurrentActiveTransform = null;
                    CurrentTargetObject = null;
                }

            }
            else if (Input.GetMouseButton(0) && CurrentAxisObject != null)
            {
                Vector3 MP = Input.mousePosition;
                MP.z = 0.3f;
                MouseSecondPosition = Camera.main.ScreenToWorldPoint(MP);
                MouseMovementDirection = MouseSecondPosition - MouseFirstPosition;


                float MagDisp = Mathf.Sign(Input.GetAxis("Mouse X")) * Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Mouse X"), 2) + Mathf.Pow(Input.GetAxis("Mouse Y"), 2));
                if (IsRotation)
                {
                    
                    Vector3 ProjectionOfMovementOnNormal = ProjectVector1On2(MouseMovementDirection, CurrentAxisNormal);
                    ProjectionOfMouseDirection = MouseMovementDirection - ProjectionOfMovementOnNormal;

                    Vector3 POMDIL = AxisTransformDataDuplicate.InverseTransformDirection(ProjectionOfMouseDirection);

                    Vector3 POMDOnTangent = ProjectVector1On2(POMDIL, CurrentTangentVectorInLocalSpace);

                    if(Vector3.Dot(POMDOnTangent,CurrentTangentVectorInLocalSpace)>=0)
                    {
                        DirectionOfRotation = 1;

                    }
                    else
                    {
                        DirectionOfRotation = -1;
                    }


                    Angle = POMDOnTangent.magnitude * 500f * RotationSpeed * Time.deltaTime * DirectionOfRotation;
                    CurrentAxisObject.transform.Rotate(Vector3.up, Angle, Space.Self);


                }
                else
                {
                    ScaleFactor = 1 + (MagDisp * Time.deltaTime * ScaleChageRate);
                    CurrentObject.transform.localScale *= ScaleFactor;
                    AxisParent.transform.localScale /= ScaleFactor;
                }
                MouseFirstPosition = MouseSecondPosition;
            }
            else if (Input.GetMouseButtonUp(0) && CurrentAxisObject != null)
            {
                Draging = false;

                
                //get target object parent as axis parent
                Transform CurrentObjectParent = CurrentAxisObject.transform.parent;
                CurrentObject.transform.parent = CurrentObjectParent;
                
                CurrentAxisObject.transform.parent = AxisParent.transform;
                CurrentAxisObject.GetComponentInChildren<AxisInfo>().ParentChanged(true);


                CurrentAxisObject = null;
                CurrentObject = null;
                AxisParent = null;
                
            }
        }
    }

    private Vector3 ProjectVector1On2(Vector3 Vector_1, Vector3 Vector_2)
    {
        Vector3 ResultingVector;

        ResultingVector = (Vector3.Dot(Vector_1, Vector_2) / (Mathf.Pow(Vector_2.magnitude, 2))) * Vector_2;

        return ResultingVector;
    }

}
