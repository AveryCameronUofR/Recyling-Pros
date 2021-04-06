﻿using System;
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
    public Text waveDescDisplay_Small;

    public Text timerDisplayHeading;
    public Text timerDisplayTime;
    public Text timerDisplayHeading_Small;
    public Text timerDisplayTime_Small;

    public Text gameoverDisplay;
    public Text livesDisplay;

    public Text introDisplay;
    public Text introDisplay_Small;
    
    public GameObject conveyor;
    public float timeBetweenWaves;

    public AudioSource audioSource;
    public AudioClip endRound;
    public AudioClip startRound;
    public bool tutorialMode;

    public AudioListener playerListener;
    public bool paused = false;
    public WaveMap currWaveMap { get; private set; }
    public GameStates currState { get; private set; }

    public readonly List<string> goodItems = new List<string> { "popcan", "tincan", "bottle", "jug" };
    public readonly List<string> badItems = new List<string> { "apple" };

    private int score = 0;
    private int playerLives;
    private readonly string waveMapDir = System.IO.Directory.GetCurrentDirectory() + "/Assets/5 - JSON_WaveScripts/";
    private List<WaveMap> waveMaps;
    public int waveIndex = 0;
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
        public int num_fences;
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
        livesDisplay.gameObject.transform.Find("Hearts").gameObject.GetComponent<Text>().text = CreateLivesString();

        conveyorCntrl = conveyor.GetComponent<conveyorController>();

        waveMaps = LoadWaves();
        currWaveMap = waveMaps[waveIndex];
        conveyorCntrl.UpdateSpeed(currWaveMap.conv_spd);

        waveDescDisplay.gameObject.SetActive(false);
        waveDescDisplay_Small.gameObject.SetActive(false);

        timerDisplayHeading.gameObject.SetActive(false);
        timerDisplayTime.gameObject.SetActive(false);
        timerDisplayHeading_Small.gameObject.SetActive(false);
        timerDisplayTime_Small.gameObject.SetActive(false);

        waveNumberDisplay.gameObject.SetActive(false);
        scoreDisplay.gameObject.SetActive(false);
        livesDisplay.gameObject.SetActive(false);

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
                    introDisplay_Small.gameObject.SetActive(false);

                    waveNumberDisplay.gameObject.SetActive(true);
                    scoreDisplay.gameObject.SetActive(true);
                    livesDisplay.gameObject.SetActive(true);
                    livesDisplay.gameObject.transform.Find("Hearts").gameObject.SetActive(true);

                    PlayAudio(startRound);
                }
                break;
            
            case GameStates.Priming:
                primeTime -= Time.deltaTime;

                if (!timerDisplayHeading.gameObject.activeSelf)
                {
                    timerDisplayHeading.gameObject.SetActive(true);
                    timerDisplayTime.gameObject.SetActive(true);
                    timerDisplayHeading_Small.gameObject.SetActive(true);
                    timerDisplayTime_Small.gameObject.SetActive(true);

                    waveDescDisplay.gameObject.SetActive(false);
                    waveDescDisplay_Small.gameObject.SetActive(false);
                }

                timerDisplayTime.text = primeTime.ToString("0.00") + "s";
                timerDisplayTime_Small.text = timerDisplayTime.text;

                if (primeTime <= 0)
                {
                    currState = GameStates.Playing;

                    primeTime = timeBetweenWaves;

                    PlayAudio(startRound);
                }
                break;
            
            case GameStates.Playing:
                if (!waveDescDisplay.gameObject.activeSelf)
                {
                    waveDescDisplay.gameObject.SetActive(true);
                    waveDescDisplay_Small.gameObject.SetActive(true);

                    timerDisplayHeading.gameObject.SetActive(false);
                    timerDisplayTime.gameObject.SetActive(false);
                    timerDisplayHeading_Small.gameObject.SetActive(false);
                    timerDisplayTime_Small.gameObject.SetActive(false);
                }

                if (currWaveMap != null)
                {
                    waveNumberDisplay.text = "Wave: " + currWaveMap.wave_id;

                    waveDescDisplay.text = currWaveMap.wave_name;
                    waveDescDisplay_Small.text = waveDescDisplay.text;
                }
                
                if (playerLives <= 0)
                    currState = GameStates.GameOver;

                break;

            case GameStates.GameOver:
                waveDescDisplay.gameObject.SetActive(false);
                waveDescDisplay_Small.gameObject.SetActive(false);

                timerDisplayHeading.gameObject.SetActive(false);
                timerDisplayTime.gameObject.SetActive(false);
                timerDisplayHeading_Small.gameObject.SetActive(false);
                timerDisplayTime_Small.gameObject.SetActive(false);

                gameoverDisplay.gameObject.SetActive(true);

                break;
        }
    }

    private List<WaveMap> LoadWaves()
    {
        string filename = tutorialMode ? "tutorialLevels.json" : "levels.json";
        string[] jsonWaves = System.IO.Directory.GetFiles(waveMapDir, filename);

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

    private void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
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
            PlayAudio(endRound);
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
        livesDisplay.gameObject.transform.Find("Hearts").gameObject.GetComponent<Text>().text = CreateLivesString();
    }

    public void Paused()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void UnPaused()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
    }


    #endregion
}
