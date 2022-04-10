using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] int numberOfAllowedLaunches = 3;
    [SerializeField] int coninPercentToWin = 60;
    private int currentNumberOfAllowedLaunches;

    private int maxCoinCount;
    private int currentCoinCount;

    private static GameController instance;
    public static GameController Instance => instance;

    [SerializeField] UIElements uIElements;
    private BirdLauncher birdLauncher;

    public bool IsGamePaused;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    void Start()
    {
        maxCoinCount = FindObjectsOfType<Coin>().Length;

        birdLauncher = FindObjectOfType<BirdLauncher>();
        birdLauncher.OnBirdLaunched += OnBirdLaunched;
        birdLauncher.OnBirdReturnedToLauncher += OnBirdReturnedToLauncher;
        
        UpdateCoinCount();
        UpdateLaunchCount();
    }

    public void OnCoinPickedUp()
    {
        currentCoinCount++;
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        uIElements.coinCountText.text = $"Coins: {currentCoinCount} / {maxCoinCount}";
    }

    private void OnBirdLaunched()
    {
        currentNumberOfAllowedLaunches++;
        UpdateLaunchCount();
    }

    private void OnBirdReturnedToLauncher()
    {
        if (currentCoinCount >= maxCoinCount)
        {
            OnWin();
            return;
        }
        
        if (currentNumberOfAllowedLaunches >= numberOfAllowedLaunches)
        {
            if (((float) currentCoinCount / maxCoinCount) * 100 > coninPercentToWin)
            {
                OnWin();
            }
            else
            {
                OnLose();
            }
        }
    }
    
    private void UpdateLaunchCount()
    {
        uIElements.launchCountText.text = $"Launches: {currentNumberOfAllowedLaunches} / {numberOfAllowedLaunches}";
    }

    private void OnWin()
    {
        IsGamePaused = true;
        ScoreController scoreController = FindObjectOfType<ScoreController>();
        uIElements.winOverlayCurrentScore.text = "Score: " + scoreController.Score;
        uIElements.winOverlayBestScore.text = "High score: " + scoreController.GetHighScore(SceneManager.GetActiveScene().name);
        
        uIElements.menuOverlay.SetActive(true);
        uIElements.winOverlay.SetActive(true);
    }

    private void OnLose()
    {
        IsGamePaused = true;
        uIElements.menuOverlay.SetActive(true);
        uIElements.loseOverlay.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        
    }

    public void GoToMainMenu()
    {
        
    }
    
    public void Pause()
    {
        IsGamePaused = true;
        uIElements.menuOverlay.SetActive(true);
        uIElements.menu.SetActive(true);
    }

    public void Unpause()
    {
        IsGamePaused = false;
        uIElements.menuOverlay.SetActive(false);
        uIElements.menu.SetActive(false);
    }
    
    private void OnDestroy()
    {
        birdLauncher.OnBirdLaunched -= OnBirdLaunched;
        birdLauncher.OnBirdReturnedToLauncher -= OnBirdReturnedToLauncher;
    }

    [System.Serializable]
    public class UIElements
    {
        public TextMeshProUGUI coinCountText;
        public TextMeshProUGUI launchCountText;

        public GameObject menuOverlay;
        
        public GameObject winOverlay;
        public TextMeshProUGUI winOverlayCurrentScore;
        public TextMeshProUGUI winOverlayBestScore;
        
        public GameObject loseOverlay;

        public GameObject menu;
    }
}
