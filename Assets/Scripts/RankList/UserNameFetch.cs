using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserNameFetch : MonoBehaviour
{
    public TMP_InputField inputField; // 引用 Unity 的 InputField 组件，用于获取用户输入的名字
    public string username; // 用于保存用户输入的名字
    void Start()
    {
        if (inputField != null) // 检查 inputField 是否为空
        {
            inputField.onEndEdit.AddListener(OnInputEndEdit);
        }
        else
        {
            Debug.LogError("InputField not assigned."); // 输出错误信息
        }
    }
        // 当用户在 InputField 中按下 Enter 键时触发的回调函数
        private void OnInputEndEdit(string text)
    {
        username = text; // 将用户输入的名字保存到变量中
        Debug.Log("用户名：" + username); // 在控制台打印保存的用户名内容
        PlayerPrefs.SetString("username", username);
        print(PlayerPrefs.GetString("username"));
    }
}