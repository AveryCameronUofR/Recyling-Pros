using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Members
    public static GameManager gm;
    public Text scoreDisplay;
    public Text waveNumberDisplay;
    public Text waveDescDisplay;
    public Text timerDisplayHeading;
    public Text timerDisplayTime;
    public float timeBetweenWaves;

    public WaveMap currWaveMap { get; private set; }
    public GameStates currState { get; private set; }

    private int score = 0;
    private readonly string waveMapDir = System.IO.Directory.GetCurrentDirectory() + "/Assets/5 - JSON_WaveScripts/";
    private List<WaveMap> waveMaps;
    private int waveIndex = 0;
    private float primeTime = 0.0f;
    

    public enum GameStates { Idle, Priming, Playing, GameOver };
    #endregion

    [Serializable]
    public class WaveMap
    {
        public int wave_id;
        public string wave_name;
        public int num_recycle;
        public int num_contam;
        public float conv_spd;
    }

    #region Private Methods

    private void Start()
    {
        if (gm == null)
            gm = this.gameObject.GetComponent<GameManager>();

        scoreDisplay.text = "Score: " + score;

        waveMaps = LoadWaves();
        currWaveMap = waveMaps[waveIndex];

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
                primeTime -= Time.deltaTime;
                if (primeTime <= 0)
                {
                    currState = GameStates.Playing;
                    primeTime = timeBetweenWaves;
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
                    
                break;
            case GameStates.GameOver:
                //Return to menu?
                break;
        }
    }

    private List<WaveMap> LoadWaves()
    {
        List<WaveMap> waveMaps = new List<WaveMap>();
        string[] jsonWaveMaps = System.IO.Directory.GetFiles(waveMapDir, "*.json");

        foreach (string jsonWaveMap in jsonWaveMaps)
        {
            string json = System.IO.File.ReadAllText(jsonWaveMap);
            WaveMap map = JsonUtility.FromJson<WaveMap>(json);

            waveMaps.Add(map);
        }

        return waveMaps;
    }

    #endregion

    #region Public Methods

    public void WaveComplete()
    {
        currState = GameStates.Priming;
        primeTime = timeBetweenWaves;
        waveIndex += 1;
        currWaveMap = waveMaps[waveIndex];
    }

    public void ItemMissed()
    {
        score -= 8;
    }

    public void ItemBinned()
    {
        score += 2;
    }

    #endregion
}
