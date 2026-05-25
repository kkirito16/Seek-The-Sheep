using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public Text stepCntText;


    [Header("axe")]
    public GameObject axeImg;
    public GameObject woodImg;
    public Text woodNumText;

    [Header("dialog")]
    public RectTransform dialogUI;
    public RawImage dialogBackground;
    public GameObject dialogTextGameObject;
    public float moveY = 120f, showUpTime = 0.5f;
    public float stayTime;
    public Text DialogText;

    LevelState_ISO iso;


    Tweener _tweener, _tweener1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        dialogBackground = dialogUI.transform.GetComponent<RawImage>();
        dialogTextGameObject = dialogUI.transform.GetChild(0).gameObject;

        _tweener = dialogUI.DOLocalMove(dialogUI.localPosition, showUpTime).SetAutoKill(false);
        _tweener1 = dialogBackground.DOColor(Color.clear, showUpTime).SetAutoKill(false);

        iso = Resources.Load("LevelState_ISO") as LevelState_ISO;

        if (SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
            BGMManager.instance.ChangeBGM(BGMtype.special_1);
        else if (SceneManager.GetActiveScene().buildIndex > 8 && SceneManager.GetActiveScene().buildIndex < 12)
            BGMManager.instance.ChangeBGM(BGMtype.special_2);
        else
            BGMManager.instance.ChangeBGM(BGMtype.main);
    }

    private void Start()
    {
        stayTime = NextLevelButton.instance.dialogStayTime;
        ShowDialogWhenOpen();


       

    }



    public void GetAxe()
    {
        axeImg.SetActive(true);
        woodImg.SetActive(true);
        woodNumText.text = "0";
    }

    public void ReturnAxe()
    {
        axeImg.SetActive(false);
        woodImg.SetActive(false);
        woodNumText.text = "0";
    }

    public void UpdateWoodNum(int num)
    {
        woodNumText.text = num.ToString();
    }

    public void UpdateStepNum(int num)
    {
        stepCntText.text = "STEP: " + num.ToString();
    }


    public void UpdateDialogAndShow(string text, float stayTime)
   {
        DialogText.text = text;

        StartCoroutine(UIShowUp(stayTime));
   }


    IEnumerator UIShowUp(float stayTime)
    {
        //_tweener.ChangeEndValue(dialogUI.localPosition + new Vector3(0, -moveY, 0), true).Play();
        _tweener1.ChangeEndValue(Color.white, true).Play();
        yield return new WaitForSeconds(showUpTime*0.2f);
        dialogTextGameObject.SetActive(true);
        yield return new WaitForSeconds(stayTime);


        //_tweener.ChangeEndValue(dialogUI.localPosition + new Vector3(0, moveY, 0), true).Play();
        
        _tweener1.ChangeEndValue(Color.clear, true).Play();
        dialogTextGameObject.SetActive(false);

        yield return new WaitForSeconds(stayTime);
    }

    public void ShowDialogWhenOpen()
    {
        int idx = SceneManager.GetActiveScene().buildIndex;

        if (iso.Dialogs[idx - 2].enterLevelDialog != "null")
            UpdateDialogAndShow(iso.Dialogs[idx - 2].enterLevelDialog, stayTime);
    }

}
