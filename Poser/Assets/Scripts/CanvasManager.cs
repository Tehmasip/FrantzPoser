using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject ButtonPrefab=null;
    public GameObject ParentForButton=null;
    CameraScript2 MyCameraScript = null;
    GameObject[] EditGameObj = null;


    private void Awake()
    {
        if(MyCameraScript==null)
        {
            MyCameraScript = GameObject.FindObjectOfType<CameraScript2>();
        }

        SelectJoints[] Foundobj= GameObject.FindObjectsOfType<SelectJoints>();
        if (Foundobj.Length > 0)
        {
            EditGameObj = new GameObject[Foundobj.Length];
            for (int i = 0; i < Foundobj.Length; i++)
            {
                EditGameObj[i] = Foundobj[i].gameObject;
                AddButtonForObject(i);
            }
        }
    }
    public void FlipCameraControls()
    {
        MyCameraScript.FlipCamControls();
    }
    private void AddButtonForObject(int i)
    {
        if(ButtonPrefab!=null)
        {
            GameObject BF = Instantiate(ButtonPrefab, ParentForButton.GetComponent<RectTransform>());
            Text Ob = BF.GetComponentInChildren<Text>();
            Ob.text = EditGameObj[i].transform.parent.parent.gameObject.name;
            Button OfBF= BF.GetComponent<Button>();
            OfBF.onClick.AddListener(delegate { SetObjectAsFocus(i); });
        }
    }
    public void SetObjectAsFocus(int i)
    {
        foreach(GameObject J in EditGameObj)
        {
            J.GetComponent<SelectJoints>().FlipactiveStates(false);
        }

        EditGameObj[i].GetComponent<SelectJoints>().FlipactiveStates(true);
        MyCameraScript.SetCurrentTargetObject(EditGameObj[i]);




    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
