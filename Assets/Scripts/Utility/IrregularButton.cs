using UnityEngine;
using UnityEngine.UI;

public class IrregularButton : MonoBehaviour
{
    private Image image;


    private void Awake()
    {
        image = transform.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.1f;
    }
}