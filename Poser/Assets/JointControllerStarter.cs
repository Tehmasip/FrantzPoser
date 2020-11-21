using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointControllerStarter : MonoBehaviour
{
   [SerializeField] private Transform[] alltransfroms;
    
    // Start is called before the first frame update
    void Start()
    {
        alltransfroms = this.gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform t in alltransfroms)
        {
            // if (t.gameObject.name.ToLower().Contains("leg") || t.gameObject.name.ToLower().Contains("spine") || t.gameObject.name.ToLower().Contains("neck") || t.gameObject.name.ToLower().Contains("arm") || t.gameObject.name.ToLower().Contains("righthand") || t.gameObject.name.ToLower().Contains("toe"))
            if (t.gameObject.tag == "Joint")
            {
                // Debug.Log(t.gameObject.name);
                t.gameObject.AddComponent<SelectJoint>();
            }
        }
    }
}
