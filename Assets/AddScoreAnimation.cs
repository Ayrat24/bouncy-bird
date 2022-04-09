using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AddScoreAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private float finalScale = 1.2f;
    [SerializeField] private float moveDistance = 2f;

    public void SetScore(int score)
    {
        text.text = "+" + score;
    }
    
    private void OnEnable()
    {
        Color color = text.color;
        color.a = 1;
        text.DOColor(color, animationDuration / 2);
        text.transform.DOMoveY(transform.position.y + moveDistance, animationDuration);

        color.a = 0;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(text.transform.DOScale(finalScale, animationDuration));
        mySequence.Append(text.DOColor(color, animationDuration / 2));
        mySequence.AppendCallback(() => gameObject.SetActive(false));

        mySequence.Play();
    }

    private void OnDisable()
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;
        text.transform.localScale = Vector3.one;
    }
}
