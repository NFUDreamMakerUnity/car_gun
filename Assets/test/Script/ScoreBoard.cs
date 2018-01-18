using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    static ScoreBoard scoreBoard;
    public PlayerScore[] playerScores = new PlayerScore[10];
    public Text[] nameText = new Text[10];
    public Text[] scoreText = new Text[10];

    DatabaseReference reference;
    int testNum = 0;

    private void Awake()
    {
        if (scoreBoard == null)
        {
            scoreBoard = this;
        }
    }

    public void getCurrentScoreBoard()
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            nameText[i].text = playerScores[i].playerName;
            scoreText[i].text = playerScores[i].playerScore.ToString();
        }
    }

    private void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("isNoNet");
            return;
        }
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vrgame-a2101.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        LoadScore();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            int a = Random.Range(10, 200);
            Debug.Log(a);
            CompareScore("ggg", a);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveScore();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadScore();
            getCurrentScoreBoard();
        }
    }

    public void CompareScore(string name, int score)
    {
        if (score < playerScores[9].playerScore)
            return;

        int index = 0;
        for (int i = 0; i < playerScores.Length; i++)
        {
            if (score <= playerScores[i].playerScore)
            {
                index++;
            }
            else
            {                
                break;
            }
        }
        for (int i = playerScores.Length - 1; i > index; i--)
        {
            playerScores[i].playerName = playerScores[i - 1].playerName;
            playerScores[i].playerScore = playerScores[i - 1].playerScore;
        }
        playerScores[index].playerScore = score;
        playerScores[index].playerName = name;
        getCurrentScoreBoard();
    }

    void SaveScore()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("isNoNet");
            return;
        }

        for (int i = 0; i < playerScores.Length; i++)
        {
            string json = JsonUtility.ToJson(playerScores[i]);
            reference.Child(i.ToString()).SetRawJsonValueAsync(json);
        }
    }

    void LoadScore()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("isNoNet");
            return;
        }

        for (int i = 0; i < playerScores.Length; i++)
        {
            getScore(i);
        }
    }

    void getScore(int i)
    {
        PlayerScore InputData = new PlayerScore();
        FirebaseDatabase.DefaultInstance.GetReference(i.ToString()).GetValueAsync().ContinueWith(
            task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("錯誤");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    JsonUtility.FromJsonOverwrite(snapshot.GetRawJsonValue(), InputData);
                    playerScores[i].playerScore = InputData.playerScore;
                    playerScores[i].playerName = InputData.playerName;
                }
            }
            );
    }

    public void ResetScore()
    {
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i].playerName = "???";
            playerScores[i].playerScore = 0;
        }
    }
}

[System.Serializable]
public class PlayerScore
{
    public string playerName;
    public int playerScore = 0;
}
