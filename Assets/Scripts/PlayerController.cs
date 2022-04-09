using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BirdLauncher birdLauncher;

    [SerializeField] float secondsToReturnIntoLauncher = 2f;
    [SerializeField] float minSpeed = 1f;
    float elapsedTimeWhileMinSpeed;
    bool shouldStop;
    [SerializeField] float playerSpeed;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        birdLauncher = FindObjectOfType<BirdLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rigidbody.simulated)
            return;

        playerSpeed = rigidbody.velocity.magnitude;

        if (rigidbody.velocity.magnitude < minSpeed)
        {
            elapsedTimeWhileMinSpeed += Time.deltaTime;
        }
        else
        { 
            elapsedTimeWhileMinSpeed = 0; 
        }

        if(elapsedTimeWhileMinSpeed >= secondsToReturnIntoLauncher)
        {
            elapsedTimeWhileMinSpeed = 0;
            shouldStop = true;
        }
    }

    public void AddForce(Vector3 force)
    {
        rigidbody.AddForce(force);
    }

    public void DisableRigidBody()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.simulated = false;
    }

    public void EnableRigidBody()
    {
        rigidbody.simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (shouldStop)
        // {
        //     shouldStop = false;
        //
        //     Vector3 launcherPos = transform.position;
        //     launcherPos.y += 2f;
        //     birdLauncher.transform.position = launcherPos;
        //
        //     birdLauncher.PutPlayerIntoLauncher();
        // }
    }
}
