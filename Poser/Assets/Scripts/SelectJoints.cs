using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectJoints : MonoBehaviour
{
    public List<GameObject> MyJoints = null;
    // Start is called before the first frame update
    private void Awake()
    {
        MyJoints = new List<GameObject>();
        GetAllJointsInList(gameObject);
        foreach(GameObject J in MyJoints)
        {
            J.layer = 9;
            TransformEdit CurrentEdit = J.AddComponent<TransformEdit>();
            SphereCollider CurrentCollider = J.AddComponent<SphereCollider>();
            CurrentCollider.radius = 0.05f;
            CurrentEdit.CreateTransforms();
        }
    }
    public void FlipactiveStates(bool State)
    {
        foreach(GameObject J in MyJoints)
        {
            SphereCollider PaObj = J.GetComponent<SphereCollider>();
            PaObj.enabled = State;
        }
    }

    private void GetAllJointsInList(GameObject Obj)
    {
        MyJoints.Add(Obj);
        int CCount = Obj.transform.childCount;
        if (CCount > 0)
        {
            for (int i = 0; i < CCount; i++)
            {
                GetAllJointsInList(Obj.transform.GetChild(i).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
