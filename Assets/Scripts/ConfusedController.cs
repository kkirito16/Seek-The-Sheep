using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ConfusedController : MonoBehaviour
{
    public static ConfusedController instance { get; private set; }
    public PlayerController playerController;
    public SheepController sheepController;
    public Vector3 startPointPos, endPointPos;

    public string dialog1, dialog2;

    public int state = 0;
    public float timer;
    public float interval;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);



        playerController = FindObjectOfType<PlayerController>();
        sheepController = FindObjectOfType<SheepController>();

        interval = Random.Range(10f, 20f);
    }

    private void Start()
    {
        startPointPos = playerController.transform.position;
        endPointPos = sheepController.transform.position;
    }

    private void Update()
    {
        if (playerController.hasFindSheep)
            return;

        if (timer < interval)
        {
            timer += Time.deltaTime;
        }else
        {
            interval = Random.Range(10f, 20f);
            Camera.main.transform.DOShakePosition(Random.Range(0.8f, 1.5f), Random.Range(0.1f, 0.3f));
            timer = 0;
        }



        switch (state)
        {
            case 0:
                if (playerController.transform.position == endPointPos)
                {
                    playerController.isConfused = true;
                    sheepController.transform.position = startPointPos;
                    state++;

                    UIManager.instance.UpdateDialogAndShow(dialog1, UIManager.instance.stayTime);

                }
                return;
            case 1:
                if (playerController.transform.position == startPointPos)
                {
                    playerController.isConfused = false;
                    sheepController.transform.position = endPointPos;
                    state++;

                    UIManager.instance.UpdateDialogAndShow(dialog2, UIManager.instance.stayTime);

                }

                return;

        }
    }





}
