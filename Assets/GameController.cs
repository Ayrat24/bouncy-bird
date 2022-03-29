using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] int numberOfAllowedLaunches = 3;
    private int currentNumberOfAllowedLaunches;

    private int maxCoinCount;
    private int currentCoinCount;

    private static GameController instance;
    public static GameController Instance { get => instance; }

    [SerializeField] UIElements uIElements;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);

        instance = this;
    }

    void Start()
    {
        maxCoinCount = FindObjectsOfType<Coin>().Length;
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

    public void OnBirdLaunched()
    {
        currentNumberOfAllowedLaunches++;
        UpdateLaunchCount();
    }

    public void UpdateLaunchCount()
    {
        uIElements.launchCountText.text = $"Launches: {currentNumberOfAllowedLaunches} / {numberOfAllowedLaunches}";
    }

    [System.Serializable]
    public class UIElements
    {
        public TextMeshProUGUI coinCountText;
        public TextMeshProUGUI launchCountText;
    }
}