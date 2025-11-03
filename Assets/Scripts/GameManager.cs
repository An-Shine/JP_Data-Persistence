using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    const string BESTSCORE_KEY = "BESTSCORE";
    const string BESTPLAYER_KEY = "BESTPLAYER";
    const string NONAME = "NONAME";
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField inputfield;
    string userName;
    public string UserName
    {
        get => userName;
        set => userName = (value.Length == 0) ? NONAME : value;     //userName 에 실제로 입력된 값이 없으면 NONAME 으로 받기
    }

    public string BestPlayer
    {
        get => PlayerPrefs.GetString(BESTPLAYER_KEY, NONAME);
        set => PlayerPrefs.SetString(BESTPLAYER_KEY, value);
    }

    public int BestScore
    {
        get => PlayerPrefs.GetInt(BESTSCORE_KEY, 0);
        set
        {
            if(BestScore <= value)
            {
                PlayerPrefs.SetInt(BESTSCORE_KEY, value);
                BestPlayer = userName;
            }
        }
    }


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    //싱글톤 타입으로 설정

    void Start()
    {
        bestScoreText.text = GetBestText();
    }

    public string GetBestText() => $"Best Player : {BestPlayer} : {BestScore}";            

    public void UpdateName() => userName = inputfield.text;
    

    public void ExitGame() =>  Application.Quit();


    public void StartGame()
    {
        UserName = inputfield.text;
        SceneManager.LoadScene("main");
    }

    [MenuItem("BlockGame/Reset score")]
    
    public static void ResetBestScore()
    {
        PlayerPrefs.SetInt(BESTSCORE_KEY, 0);
        PlayerPrefs.SetString(BESTPLAYER_KEY, NONAME);
    }

}