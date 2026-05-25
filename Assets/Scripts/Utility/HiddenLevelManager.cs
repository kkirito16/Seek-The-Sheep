using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Bloom = UnityEngine.Rendering.Universal.Bloom;

public class HiddenLevelManager : MonoBehaviour
{
    public GameObject nurse, sheep;
    public RawImage cg;
    
    public float changeTime = 3f;

    public string dialog_Nurse;
    public string dialog_sheep;
    Tweener _tweener;
    bool hasTriggered;

    PlayerController playerController;
    Volume volume;
    Bloom bloom;
    ColorAdjustments colorAjuestments;

    public static HiddenLevelManager instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out colorAjuestments);
    }


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        _tweener = cg.DOColor(Color.clear, changeTime).SetAutoKill(false);
    }

    private void Update()
    {
        if (!hasTriggered && playerController.transform.position == nurse.transform.position)
        {
            DOTween.To(() => colorAjuestments.saturation.value, x => colorAjuestments.saturation.value = x, 0, changeTime);
            Camera.main.transform.DOShakePosition(1.5f, 0.4f);
            hasTriggered = true;
            StartCoroutine(ColorBack());
            
        }

        
    }


    IEnumerator ColorBack()
    {

        nurse.SetActive(false);

        UIManager.instance.UpdateDialogAndShow(dialog_Nurse, UIManager.instance.stayTime);
        yield return new WaitForSeconds(UIManager.instance.stayTime);

        sheep.SetActive(true);
        UIManager.instance.UpdateDialogAndShow(dialog_sheep, UIManager.instance.stayTime);
        yield return new WaitForSeconds(UIManager.instance.stayTime);

      
    }


    public void CGShow()
    {
        StartCoroutine(CG());
    }


    IEnumerator CG()
    {
        DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 80f, changeTime*2f);
        yield return null;
        _tweener.ChangeEndValue(Color.white, true).Play();

        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }

}
