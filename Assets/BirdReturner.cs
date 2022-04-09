using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdReturner : MonoBehaviour
{
    private PlayerController bird;
    private BirdLauncher birdLauncher;

    [SerializeField] private float maxTimeInside = 1;
    private float elapsedTimeInside;
    
    [SerializeField] private float maxTimeToMove = 2;
    [SerializeField] private float maxDistanceAboveGround = 2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 launcherOffset;
    private float elapsedTimeAfterMoving;

    private bool playerIsInside;
    
    void Start()
    {
        bird = FindObjectOfType<PlayerController>();
        birdLauncher = FindObjectOfType<BirdLauncher>();

        birdLauncher.OnBirdReturnedToLauncher += OnBirdReturned;
    }

    void Update()
    {
        if(birdLauncher.LauncherActive)
            return;
    
        elapsedTimeAfterMoving += Time.deltaTime;

        if (elapsedTimeAfterMoving > maxTimeToMove)
        {
            transform.position = bird.transform.position;
        }
    }

    private void OnBirdReturned()
    {
        elapsedTimeInside = -1;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            elapsedTimeAfterMoving = 0;
            
            playerIsInside = true;
            elapsedTimeInside += Time.deltaTime;
            Debug.Log("Player stay " + elapsedTimeInside);

            if (elapsedTimeInside > maxTimeInside)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, maxDistanceAboveGround, groundLayer);
                if (hit.collider != null)
                {
                    birdLauncher.transform.position = hit.point + launcherOffset;
                    birdLauncher.PutPlayerIntoLauncher();
                }
                else
                {
                    elapsedTimeInside = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            playerIsInside = false;
            elapsedTimeAfterMoving = 0;
            elapsedTimeInside = 0;
            Debug.Log("Player exited");
        }
    }
}
