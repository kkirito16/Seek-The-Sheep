using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance { get; private set; }
    public Transform title;
    public float changeSceneTime = 0.3f;
    public Color endColor;

    public Image panelImg;
    Tweener _tweener, _tweener1;
    float timer = 0;
    float interval = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        panelImg = GetComponent<Image>();


        BGMManager.instance.ChangeBGM(BGMtype.main);
        // _tweener = panelImg.DOColor(endColor, changeSceneTime).SetAutoKill(false);
        // _tweener1 = panelImg.DOColor(endColor, changeSceneTime).SetAutoKill(false);
    }

    private void Start()
    {
        interval = Random.Range(2f, 4f);
        title.DORotate(new Vector3(0, 15, 2.5f), 2f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Yoyo);

    }

    private void Update()
    {
       
    }
    /*
    public void LoadLevel(string name)
    {
        //StartCoroutine(ChangeScene(name));
        SceneManager.LoadScene(name);
    }
    */

    public void LoadLevel(int idx)
    {
       // StartCoroutine(ChangeScene(idx));
       SceneManager.LoadScene(idx);
    }

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        //StartCoroutine(ChangeScene(scene.name));
        SceneManager.LoadScene(scene.buildIndex);
    }


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator ChangeScene(string name)
    {
       // _tweener.ChangeEndValue(endColor, true).Play();
        panelImg.DOColor(endColor, changeSceneTime);
        yield return new WaitForSeconds(changeSceneTime);

        SceneManager.LoadScene(name);
       // _tweener1.ChangeEndValue(Color.clear, true).Play();
         panelImg.DOColor(Color.clear, changeSceneTime);
         yield return new WaitForSeconds(changeSceneTime);


    }

    IEnumerator ChangeScene(int idx)
    {
       // _tweener.ChangeEndValue(endColor, true).Play();
         panelImg.DOColor(endColor, changeSceneTime);
        yield return new WaitForSeconds(changeSceneTime);

        SceneManager.LoadScene(idx);
       // _tweener1.ChangeEndValue(Color.clear, true).Play();
        panelImg.DOColor(Color.clear, changeSceneTime);
        yield return new WaitForSeconds(changeSceneTime);


    }
}
