using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static int WIN=1,LOSE=0;

    public GameObject blueBase, redBase;

    public static bool GameIsOver;
    public static int GameResult_win = 1,GameResult_Lose =0;//


    #region UI
    private GameObject gameOverUI;
    private GameObject completeLevelUI;
    public TextMeshProUGUI timerText;
    #endregion



    [SerializeField]
    AudioSource _audioSource;
    AudioSource audioSource;
    [SerializeField]
    Slider audioSlider;

    GameObject TargetBase;
    GameObject playerBase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //Application.targetFrameRate = 60;

    }
   
    void Start()
    {
        GameIsOver = false;
        if (audioSource == null)
        {
            audioSource = _audioSource;
        }
        gameOverUI.SetActive(false);
        completeLevelUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
            return;


        if (TargetBase==null)
        {
            completeLevelUI.SetActive(true);
            Time.timeScale = 0;
            WinLevel();

        }
        if (playerBase == null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
            EndGame();
        }
    }
    public void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        StageManager.instance.OnBattleComplete(LOSE);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
        StageManager.instance.OnBattleComplete(WIN);
    }
}
