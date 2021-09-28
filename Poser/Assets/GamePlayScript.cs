using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayScript : MonoBehaviour
{
    public GameObject grid;
   public bool togglebool;//bool can only store true and fale like a check box by default bool is false
    public void switchgrid()
    {
        grid.SetActive(togglebool);//f  t  f t f 

        if(togglebool == false)
        {
            togglebool = true;
        }
        else
        {
            togglebool = false;
        }
    }
}
