using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelDialog : MonoBehaviour
{
    public int caseIdx;

    public string level_1_dialog;
    Pit Pit;
    SpriteRenderer level_1_spriteRenderer;

    public string level_2_dialog;
    SpriteRenderer level_2_spriteRenderer;
    Sprite wood;

    public string level_5_dialog;
    Transform playerTransform;


    private void Awake()
    {
        switch(caseIdx)
        {
            case 0:
                level_1_spriteRenderer = GetComponent<SpriteRenderer>();
                Pit = GetComponent<Pit>();
                return;
            case 1:
                wood = GetComponent<Water>().wood;
                level_2_spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
                return;
            case 2:
                playerTransform = FindObjectOfType<PlayerController>().transform;
                return;
        }

    }

    private void Update()
    {
        switch(caseIdx)
        {
            case 0:
                if (level_1_spriteRenderer.sprite == Pit.pitWithRock)
                {
                    UIManager.instance.UpdateDialogAndShow(level_1_dialog, UIManager.instance.stayTime);
                    Destroy(this);
                }

                return;
            case 1:
                if (level_2_spriteRenderer.sprite == wood)
                {
                    UIManager.instance.UpdateDialogAndShow(level_2_dialog, UIManager.instance.stayTime);
                    Destroy(this);
                }
                return;
            case 2:
                if(playerTransform.position == new Vector3(3, -4, 0))
                {
                    UIManager.instance.UpdateDialogAndShow(level_5_dialog, UIManager.instance.stayTime);
                    Destroy(this);
                }

                return;

        }
        
    }
}
