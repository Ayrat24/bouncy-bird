using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spring : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1;        
    [SerializeField] private float speedMult = 1.2f;        
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            Rigidbody2D rigid = col.GetComponent<Rigidbody2D>();

            float jumpVelocity = rigid.velocity.magnitude * speedMult;
            rigid.velocity = Vector2.zero;
            rigid.velocity = transform.up * (jumpVelocity > minSpeed ? jumpVelocity : minSpeed);
        }
    }
}
