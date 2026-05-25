using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public CanvasGroup canvasGroup; // Assign your CanvasGroup in the inspector

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0f; // Set the panel to be hidden initially
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1; // Toggle the panel's visibility
        }
    }
}
