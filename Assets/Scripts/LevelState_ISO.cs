using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelState_ISO", menuName = "Data / LevelState_ISO")]
public class LevelState_ISO : ScriptableObject
{
    public List<bool> levelState = new List<bool>();
    public List<Dialog> Dialogs = new List<Dialog>();

    public List<ButtonImg>  buttonImgs = new List<ButtonImg>();

}


[System.Serializable]
public class Dialog
{
    public string name;
    public string findSheepDialog;
    public string enterLevelDialog;
    public Texture sheep;
}

[System.Serializable]

public class ButtonImg
{
    public Sprite bloom;
    public Sprite grey;
}