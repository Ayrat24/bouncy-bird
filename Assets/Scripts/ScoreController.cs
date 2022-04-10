using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int score;
    public int Score => score;
    
    [SerializeField] private AddScoreAnimation scoreAnimation;
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    
    private static ScoreController instance;
    public static ScoreController Instance => instance;

    [SerializeField] private int bonusScoreForCombo = 50;
    private int currentComboBonus;

    private BirdLauncher launcher;
    private Sequence addScoreSequence;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        launcher = FindObjectOfType<BirdLauncher>();
        launcher.OnBirdReturnedToLauncher += OnBirdReturnedIntoLauncher;
        UpdateScoreText(0);
    }
    
    private void OnBirdReturnedIntoLauncher()
    {
        currentComboBonus = 0;
    }

    public void AddScore(int scoreToAdd, Vector3 position)
    {
        
        AddScoreAnimation anim = Instantiate(scoreAnimation, position, Quaternion.identity, parentCanvas.transform);

        currentComboBonus += bonusScoreForCombo;
        anim.SetScore(scoreToAdd + currentComboBonus);
        anim.gameObject.SetActive(true);

        UpdateScoreText(scoreToAdd + currentComboBonus);
    }

    private void UpdateScoreText(int scoreToAdd)
    {
        //int currentlyDisplayedScore = int.Parse(scoreText.text);
        //addScoreSequence.Kill();
        int oldScore = score;
        score += scoreToAdd;
        
        addScoreSequence = DOTween.Sequence();
        addScoreSequence.Append(scoreText.transform.DOScale(1.2f, 0.2f));
        addScoreSequence.Append(DOTween
            .To(x => oldScore = (int)x, oldScore, score, 0.5f)
            .OnUpdate(() => scoreText.text = "Score: " + oldScore));
        addScoreSequence.Append(scoreText.transform.DOScale(1f, 0.2f));
        addScoreSequence.Play();
    }

    public int GetHighScore(string sceneName)
    {
        int highScore = PlayerPrefs.GetInt("HighScore" + sceneName, 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore" + sceneName, score);
        }

        return highScore;
    }
}
