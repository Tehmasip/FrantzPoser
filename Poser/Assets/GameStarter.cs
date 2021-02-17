using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainGame;
    public GameObject MainMenu;
    public void StartGame()
    {
        MainGame.SetActive(true);
        MainMenu.SetActive(false);
    }
}
