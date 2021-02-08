using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentHandler : MonoBehaviour
{
    public GameObject[] envs;
    // Start is called before the first frame update
    int i;

    public void NextEnv()
    {

        envs[i].SetActive(false);
        i++;
        if (i == envs.Length)
            i = 0;

        envs[i].SetActive(true);

    }

    public void BackEnv()
    {
        envs[i].SetActive(false);
        i--;
        if (i == 0)
            i = envs.Length-1;

        envs[i].SetActive(true);
    }
}
