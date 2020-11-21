using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public void XRotationButton()
    {
        RotateWithDrag.Instance.xRotation = true;
        RotateWithDrag.Instance.yRotation = false;
    }

    public void YRotationButton()
    {
        RotateWithDrag.Instance.xRotation = false;
        RotateWithDrag.Instance.yRotation = true;
    }
}
