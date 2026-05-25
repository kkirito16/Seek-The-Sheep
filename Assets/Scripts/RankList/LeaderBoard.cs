using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker;
using LootLocker.Requests;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public string leaderboardKey = "16304";
    public string sceneKey;
    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    public int localPlayerScore = 1000; // 本地玩家分数

    // Start is called before the first frame update
    void Start()
    {
        localPlayerScore = PlayerPrefs.GetInt(sceneKey, 1000); // 获取本地玩家分数
    }


    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        if (scoreToUpload >= localPlayerScore) // 如果要上传的分数小于等于本地分数，则不上传分数
        {
            yield break;
        }

        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                localPlayerScore = scoreToUpload; // 更新本地玩家分数
                //PlayerPrefs.SetInt("LocalPlayerScore", localPlayerScore); // 将本地玩家分数保存到本地
                PlayerPrefs.SetInt(sceneKey, localPlayerScore); // 将本地玩家分数保存到本地
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData?.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }


    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Steps\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.errorData?.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}