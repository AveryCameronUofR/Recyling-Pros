using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    #region Members
    public static GameManager gm;
    public int startingLives;
    public Text scoreDisplay;
    public Text waveNumberDisplay;
    public Text waveDescDisplay;
    public Text timerDisplayHeading;
    public Text timerDisplayTime;
    public Text gameoverDisplay;
    public Text livesDisplay;
    public Text introDisplay;
    public GameObject conveyor;
    public float timeBetweenWaves;

    public WaveMap currWaveMap { get; private set; }
    public GameStates currState { get; private set; }

    public readonly List<string> goodItems = new List<string> { "popcan", "tincan" };
    public readonly List<string> badItems = new List<string> { "apple" };

    private int score = 0;
    private int playerLives;
    private readonly string waveMapDir = System.IO.Directory.GetCurrentDirectory() + "/Assets/5 - JSON_WaveScripts/";
    private List<WaveMap> waveMaps;
    private int waveIndex = 0;
    private float primeTime = 0.0f;
    private bool exitIdle = false;
    private conveyorController conveyorCntrl;
    private string heart_symbol = "\u2764";

    public enum GameStates { Idle, Priming, Playing, GameOver };
    #endregion

    public class Levels 
    {
        public WaveMap[] levels;
    }

    [Serializable]
    public class WaveMap
    {
        public int wave_id;
        public string wave_name;
        public int num_recycle;
        public int num_contam;
        public int num_bombs;
        public int num_nets;
        public int num_spray_cans;
        public float conv_spd;
        public int[] spawn_delays;
    }

    #region Private Methods

    private void Start()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        scoreDisplay.text = "Score: " + score;

        playerLives = startingLives;
        livesDisplay.text = CreateLivesString();

        conveyorCntrl = conveyor.GetComponent<conveyorController>();

        waveMaps = LoadWaves();
        currWaveMap = waveMaps[waveIndex];
        conveyorCntrl.UpdateSpeed(currWaveMap.conv_spd);

        waveDescDisplay.gameObject.SetActive(false);
        timerDisplayHeading.gameObject.SetActive(false);
        timerDisplayTime.gameObject.SetActive(false);

        currState = GameStates.Idle;

        primeTime = timeBetweenWaves;
    }

    private void Update()
    {
        scoreDisplay.text = "Score: " + score;

        switch (currState)
        {
            case GameStates.Idle:
                if (exitIdle)
                {
                    currState = GameStates.Playing;
                    introDisplay.gameObject.SetActive(false);
                }
                break;
            case GameStates.Priming:
                primeTime -= Time.deltaTime;

                if (!timerDisplayHeading.gameObject.activeSelf)
                {
                    timerDisplayHeading.gameObject.SetActive(true);
                    timerDisplayTime.gameObject.SetActive(true);
                    waveDescDisplay.gameObject.SetActive(false);
                }

                timerDisplayTime.text = primeTime.ToString("0.00") + "s";

                if (primeTime <= 0)
                {
                    currState = GameStates.Playing;
                    primeTime = timeBetweenWaves;
                }
                break;
            case GameStates.Playing:
                if (!waveDescDisplay.gameObject.activeSelf)
                {
                    waveDescDisplay.gameObject.SetActive(true);
                    timerDisplayHeading.gameObject.SetActive(false);
                    timerDisplayTime.gameObject.SetActive(false);
                }

                if (currWaveMap != null)
                {
                    waveNumberDisplay.text = "Wave: " + currWaveMap.wave_id;
                    waveDescDisplay.text = currWaveMap.wave_name;
                }
                
                if (playerLives <= 0)
                {
                    currState = GameStates.GameOver;
                }

                break;
            case GameStates.GameOver:
                waveDescDisplay.gameObject.SetActive(false);
                timerDisplayHeading.gameObject.SetActive(false);
                timerDisplayTime.gameObject.SetActive(false);

                gameoverDisplay.gameObject.SetActive(true);

                break;
        }
    }

    private List<WaveMap> LoadWaves()
    {
        string[] jsonWaves = System.IO.Directory.GetFiles(waveMapDir, "levels.json");

        string json = System.IO.File.ReadAllText(jsonWaves[0]);
        Levels levels = JsonUtility.FromJson<Levels>(json);

        List<WaveMap> waveMaps = new List<WaveMap>(levels.levels);

        return waveMaps;
    }

    private string CreateLivesString()
    {
        string livesStr = "";

        if (playerLives <= 0)
            return livesStr;

        for (int i = 0; i < playerLives; i++)
            livesStr += heart_symbol + " ";

        livesStr.Remove(livesStr.Length - 1);
        return livesStr;
    }

    #endregion

    #region Public Methods

    public void ExitIdle()
    {
        exitIdle = true;
    }

    public void WaveComplete()
    {
        if (playerLives > 0)
        {
            currState = GameStates.Priming;
            primeTime = timeBetweenWaves;
            waveIndex += 1;
            currWaveMap = waveMaps[waveIndex];
            conveyorCntrl.UpdateSpeed(currWaveMap.conv_spd);
        }
    }

    public void DecreaseScore()
    {
        score -= 4;
    }

    public void IncreaseScore()
    {
        score += 2;
    }

    public void ItemMissed()
    {
        playerLives -= 1;
        livesDisplay.text = CreateLivesString();
    }

    #endregion
}
