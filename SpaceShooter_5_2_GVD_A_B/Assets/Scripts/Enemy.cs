﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // create a variable for the speed of the enemy
    [SerializeField]
    private float _speed = 4f;
    private Player _player;

    Animator _enemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player script is null");
        }
        _enemyAnimator = gameObject.GetComponent<Animator>();
        if(_enemyAnimator == null)
        {
            Debug.LogError("Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //translate the enemy position down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if the enemy has gone below -6 on the y axis
        if(transform.position.y < -6f)
        {
            //Vector3 teleport = new Vector3(transform.position.x,7.5f,0)
            //transform.position = teleport
            float randomX = Random.Range(-9.2f,9.2f);
            transform.position = new Vector3(randomX,7.5f,0);
        }
        //change the position to teleport from the top of the screen from 7.5f on the y axis
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Debug.Log("Hit: " + other.transform.name);
        if(other.tag == "Laser")
        {
            //Destroy the laser
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Destroy the enemy
            Destroy(this.gameObject,2.633f);
        }
        if(other.tag == "Player")
        {
            //Damage the player
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            _enemyAnimator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            gameObject.GetComponent<Collider2D>().enabled = false;
            //Destroy the enemy
            Destroy(this.gameObject,2.633f);
        }
    }
}
