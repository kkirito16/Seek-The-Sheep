using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class BlackWhiteScreen : MonoBehaviour
{
    [Range(0, 1)]
    public float BwBlend = 1f;
    public Material material;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_bwBlend", BwBlend);
        //Graphics.Blit 是一个屏幕处理函数,默认会用当前摄像机渲染的图形为 _MainTex赋值
        //所以再这里我们只需要为_bwBlend 赋值
        Graphics.Blit(source, destination, material);
    }

}
