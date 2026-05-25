using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PM : MonoBehaviour
{
    public LeaderBoard leaderboard;
    public TMP_InputField playerNameInputfield;

    private float fetchInterval = 5.0f; // �����ʱ����

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(PlayerPrefs.GetString("username"), (response) =>
        // LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + response.errorData?.message);
            }
        });
    }

    [System.Obsolete]
    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
      //  StartCoroutine(FetchHighscoresRoutine());
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    [System.Obsolete]
    IEnumerator FetchHighscoresRoutine()
    {
        while (true)
        {
            yield return leaderboard.FetchTopHighscoresRoutine();
            yield return new WaitForSeconds(fetchInterval);
        }
    }
}