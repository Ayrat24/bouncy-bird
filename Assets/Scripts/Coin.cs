using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int score = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            GameController.Instance.OnCoinPickedUp();
            ScoreController.Instance.AddScore(score, transform.position);
            gameObject.SetActive(false);
        }
    }
}