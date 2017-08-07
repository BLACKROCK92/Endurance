using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameController : NetworkBehaviour
{
    public static GameController instance = null;

    [SyncVar(hook = "UpdateScoreText")]
    int score;
    Text scoreTxt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        scoreTxt = GameObject.Find("ScorePointsTxt").GetComponent<Text>();

        DontDestroyOnLoad(gameObject);
    }

    [Command]
    public void CmdModifyScore(int amount)
    {
        score += amount;
    }

    void UpdateScoreText(int scoreValue)
    {
        scoreValue = score;
        scoreTxt.text = scoreValue.ToString();
    }

}
