using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAllLevels : MonoBehaviour
{
    public bool cliked;
    LevelState_ISO iso;
    // Start is called before the first frame update
   
    public void UnlockAll()
    {
        iso = Resources.Load("LevelState_ISO") as LevelState_ISO;
        {
            for (int i = 0; i < iso.levelState.Count-1; i++)
            {
                iso.levelState[i] = true;
                LevelManager.instance.buttons[i].interactable = true;

            }
        }
        cliked = true;

        LevelManager.instance.buttons[9].gameObject.SetActive(true);
        LevelManager.instance.buttons[9].interactable = true;
    }


    private void Update()
    {
        if (cliked)
        {
            for (int i = 0; i < iso.levelState.Count-1; i++)
            {
                iso.levelState[i] = true;
                LevelManager.instance.buttons[i].interactable = true;

            }
            LevelManager.instance.buttons[9].interactable = true;
        }
    }
}
