using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformEdit : MonoBehaviour
{
    public float ScaleFactor = 0.1f;
    public GameObject CircleSpritePref;
    public GameObject SquarePref;
    private GameObject XaxisCircle = null;
    private GameObject YaxisCircle = null;
    private GameObject ZaxisCircle = null;
    private GameObject ScaleSquare = null;

    public GameObject ParentObject = null;

    // Start is called before the first frame update
    public void CreateTransforms()
    {
        if (ParentObject == null)
        {

            ParentObject = new GameObject();
            ParentObject.transform.parent = transform;
            ParentObject.transform.localPosition = Vector3.zero;
            ParentObject.transform.localRotation = Quaternion.identity;
            ParentObject.transform.localScale = Vector3.one;
            ParentObject.name = "TransformEdits";
            ParentObject.tag = "TransEdit";

        }
        if(GameObject.ReferenceEquals(CircleSpritePref,null))
        {
            Debug.Log("Circle is null");
        }
        if(CircleSpritePref!=null)
        {
            Debug.Log("Here");
            XaxisCircle = Instantiate(CircleSpritePref, ParentObject.transform);
            XaxisCircle.transform.localRotation = Quaternion.Euler(0f, 0f, -90f);
            XaxisCircle.gameObject.GetComponentInChildren<MeshRenderer>().materials[0].color = new Color(1f,0f,0f,0.4f);
            XaxisCircle.gameObject.name = "XaxisCircle";
            XaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Axis = 0;
            XaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Target = gameObject;
            XaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Type = true;

            YaxisCircle = Instantiate(CircleSpritePref, ParentObject.transform);
            YaxisCircle.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            YaxisCircle.gameObject.GetComponentInChildren<MeshRenderer>().materials[0].color = new Color(0f, 1f, 0f, 0.4f);
            YaxisCircle.gameObject.name = "YaxisCircle";
            YaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Axis = 1;
            YaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Type = true;
            YaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Target = gameObject;

            ZaxisCircle = Instantiate(CircleSpritePref, ParentObject.transform);
            ZaxisCircle.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            ZaxisCircle.gameObject.GetComponentInChildren<MeshRenderer>().materials[0].color = new Color(0f, 0f, 1f, 0.4f);
            ZaxisCircle.gameObject.name = "ZaxisCircle";
            ZaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Axis = 2;
            ZaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Type = true;
            ZaxisCircle.gameObject.GetComponentInChildren<AxisInfo>().Target = gameObject;
        }
        if(SquarePref!=null)
        {
            ScaleSquare = Instantiate(SquarePref, ParentObject.transform);
            ScaleSquare.transform.localPosition = Vector3.zero;
            ScaleSquare.transform.localRotation = Quaternion.identity;
            ScaleSquare.gameObject.GetComponentInChildren<AxisInfo>().Type = false;
            ScaleSquare.gameObject.GetComponentInChildren<AxisInfo>().Target = gameObject;
            //ScaleSquare.transform.localScale = Vector3.one;
        }

        if(ParentObject!=null)
        {
            ParentObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float Size = (Camera.main.transform.position - transform.position).magnitude * ScaleFactor;
        ParentObject.transform.localScale = new Vector3(Size, Size, Size);
    }
}
