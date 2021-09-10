using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public CameraScript2 CamScript;
    public GameObject CamButton;
    public GameObject BobyControlButton;
    public GameObject SelectGenderPanel;
    public GameObject Squareprefeb;
    public GameObject CirclePrefeb;
    public GameObject selectModelScreen;

    public GameObject MoverGameObject;
    public GameObject ButtonPrefab = null;
    public GameObject ParentForButton = null;
    CameraScript2 MyCameraScript = null;
    //GameObject[] EditGameObj = null;
    //Button[] ModelButtons = null;
    public List<Button> ModelButtons;
    public GameObject[] ModelPrefebs;

    public List<GameObject> EditGameObj;
    private void Awake()
    {
        if (MyCameraScript == null)
        {
            MyCameraScript = GameObject.FindObjectOfType<CameraScript2>();
        } 
        
        SelectJoints[] Foundobj = GameObject.FindObjectsOfType<SelectJoints>();

        if (Foundobj.Length > 0)
        {
           // EditGameObj = null;
            //EditGameObj.Add(); = new GameObject[Foundobj.Length];
            for (int i = 0; i < Foundobj.Length; i++)
            {
               // EditGameObj[i] = ;
                EditGameObj.Add(Foundobj[i].gameObject);
                
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
        if (ButtonPrefab != null)
        {
            GameObject BF = Instantiate(ButtonPrefab, ParentForButton.GetComponent<RectTransform>());
            // Text Ob = BF.GetComponentInChildren<Text>();
            //  Ob.text = EditGameObj[i].transform.parent.parent.gameObject.name;
            Button OfBF = BF.GetComponent<Button>();
            ModelButtons.Add(OfBF);
            OfBF.onClick.AddListener(delegate { SetObjectAsFocus(i); });
        }
    }

    public void SetObjectAsFocus(int i)
    {
        foreach (GameObject J in EditGameObj)
        {
            J.GetComponent<SelectJoints>().FlipactiveStates(false);
        }
        EditGameObj[i].GetComponent<SelectJoints>().myanim.enabled = false;
        EditGameObj[i].GetComponent<SelectJoints>().FlipactiveStates(true);
        MyCameraScript.SetCurrentTargetObject(EditGameObj[i]);

        EnableMoverOnPlayer(EditGameObj[i]);

        MoverGameObject.GetComponent<TargetCarry>().Target = EditGameObj[i].transform.parent.parent;
    }
    private void Update()
    {
        
    }
    public void EnableMoverOnPlayer(GameObject target)
    {
        Vector3 temp = target.transform.position;
        MoverGameObject.transform.position = temp + new Vector3(0, -1, 0);
    }
    public void PressButtonModel(int num)
    {
      
      SelectGenderPanel.SetActive(false);
      GameObject TempModel =  Instantiate(ModelPrefebs[num]);
      GameObject temp2 = TempModel.GetComponentInChildren<SelectJoints>().gameObject;
      EditGameObj.Add(temp2);
      Debug.Log("EditGameObj.Count-1" + (EditGameObj.Count - 1));
      AddButtonForObject(EditGameObj.Count-1);
    }

    public void SwitchToCam()
    {
        FlipCameraControls();

        CamButton.SetActive(false);
        BobyControlButton.SetActive(true);
        SetMoverDisplay();
    }
    public void SwitchToPlayer()
    {
        FlipCameraControls();
        CamButton.SetActive(true);
        BobyControlButton.SetActive(false);
        SetMoverDisplay();
    }
    public Animator currentAnimator;
    public string[] Anim;
    public int index;
    public void ChangeAnimation(int num)
    {
        if(CamScript.CurrentCameraTarget.GetComponent<SelectJoints>()!=null)
        {
            
            currentAnimator = CamScript.CurrentCameraTarget.GetComponent<SelectJoints>().myanim;
            currentAnimator.enabled = true;
            currentAnimator.Play(Anim[num]);
            index = num;
        }
    }
    public void pauseAnimation()
    {
        currentAnimator.speed = 0;
    }
    public void playAnimation()
    {
        currentAnimator.speed = 1;
    }

    public Scrollbar bar;
    public void ChangeFrame()
    {
        currentAnimator.Play(Anim[index], 0, bar.value);
        currentAnimator.speed = 0;
    }
    public void SetMoverDisplay()
    {
        if (MoverGameObject.activeSelf)
            MoverGameObject.SetActive(false);
        else

            MoverGameObject.SetActive(true);
    }
}