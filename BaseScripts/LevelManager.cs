using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //En metod som ska starta om scenen.
    public void Restart(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    //En metod som stänger av spelet.
    public void QuitButton()
    {
        Application.Quit();
    }

}
