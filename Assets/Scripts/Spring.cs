using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spring : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float minSpeed = 1;        
    [SerializeField] private float speedMult = 1.2f;
    [SerializeField] private float cooldown = 0.5f;
    private float elapsedTime;
    
    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(elapsedTime < cooldown)
            return;
        else
            elapsedTime = 0;
        
        if (col.tag.Equals("Player"))
        {
            Rigidbody2D rigid = col.GetComponent<Rigidbody2D>();

            float jumpVelocity = rigid.velocity.magnitude * speedMult;
            //rigid.velocity = Vector2.zero;
            //rigid.velocity =  arrow.transform.right * (jumpVelocity > minSpeed ? jumpVelocity : minSpeed);

            rigid.velocity = arrow.transform.right * minSpeed;
        }
    }
}
