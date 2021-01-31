using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoverScript : MonoBehaviour
{
    public TargetCarry targetCarry;
    public Material mat;
    Color MainColor;
    
    public Transform ParentMover;
    private Vector3 screenPoint;
    private Vector3 offset;

    public int moveDir;

    public bool OnClickDown;
    void OnMouseDown()
    {
        OnClickDown = true;
        MainColor = mat.color;
        mat.color = Color.yellow;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    /*
    void OnMouseDrag()
    {

      //  if(moveDir == 1)
      //  {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x,0, 0);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            Target.position = new Vector3(cursorPosition.x, Target.position.y,Target.position.z);
            ParentMover.position = new Vector3(cursorPosition.x, Target.position.y, Target.position.z);
        /* }
         if (moveDir == 2)
         {
             Vector3 cursorScreenPoint = new Vector3(0,0,Input.mousePosition.x);
             Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
             Target.position = cursorPosition;
             ParentMover.position = cursorPosition;
         }
         if(moveDir == 3)
         {
             Vector3 cursorScreenPoint = new Vector3(0,Input.mousePosition.y,0);
             Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
             Target.position = cursorPosition;
             ParentMover.position = cursorPosition;
         }

    }*/
    private void Update()
    {
        if (OnClickDown && moveDir == 1)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            targetCarry.Target.position = new Vector3(cursorPosition.x, targetCarry.Target.position.y, targetCarry.Target.position.z);
            ParentMover.position = new Vector3(cursorPosition.x, targetCarry.Target.position.y, targetCarry.Target.position.z);
        }
        else if(OnClickDown && moveDir == 2)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            targetCarry.Target.position = new Vector3(targetCarry.Target.position.x, cursorPosition.y, targetCarry.Target.position.z);
            ParentMover.position = new Vector3(targetCarry.Target.position.x, cursorPosition.y, targetCarry.Target.position.z);
        }
        else if(OnClickDown && moveDir == 3)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            targetCarry.Target.position = new Vector3(targetCarry.Target.position.x, targetCarry.Target.position.y, cursorPosition.z);
            ParentMover.position = new Vector3(targetCarry.Target.position.x, targetCarry.Target.position.y, cursorPosition.z);
        }
    }
    void OnMouseUp()
    {
        OnClickDown = false;
        mat.color = MainColor;
    }
}
