using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCOntrolelrSCript : MonoBehaviour
{
    public Animator CubeAnimator;
    // Start is called before the first frame update
   public void SetParameter()
    {
       // CubeAnimator.SetBool("DoShrink",true);
        CubeAnimator.Play("shirkexpandanimation");
    }
}
