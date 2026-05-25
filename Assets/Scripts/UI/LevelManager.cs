using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    public Image BackgrroundImg;
    public Sprite brightImg;

    public List<Button> buttons;

    public LevelState_ISO iso;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        iso = Resources.Load("LevelState_ISO") as LevelState_ISO;


        if (iso.levelState[0] == true)
            buttons[0].GetComponent<Image>().sprite = iso.buttonImgs[0].bloom;
        else
            buttons[0].GetComponent<Image>().sprite = iso.buttonImgs[0].grey;

        for (int i = 1; i < iso.levelState.Count; i++)
        {
            if (iso.levelState[i] == true)
                buttons[i].GetComponent<Image>().sprite = iso.buttonImgs[i].bloom;
            else
                buttons[i].GetComponent<Image>().sprite = iso.buttonImgs[i].grey;


            if (iso.levelState[i-1] == true)
                buttons[i].interactable = true;

        }

        if (iso.levelState[8] == true)
            buttons[9].gameObject.SetActive(true);
        if (iso.levelState[9] == true)
        { 
            BackgrroundImg.sprite = brightImg;
        }

    }


    public void UnLockHiddenLevel()
    {
        buttons[buttons.Count-1].gameObject.SetActive(true);
    }
}
