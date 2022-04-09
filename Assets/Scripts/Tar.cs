using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tar : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            if(playerRigidBody == null)
                playerRigidBody = col.GetComponent<Rigidbody2D>();
            
            playerRigidBody.velocity *= 0.5f;
        }
    }
}
