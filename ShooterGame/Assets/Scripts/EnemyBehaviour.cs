using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public float enemySpeed = 1;
    public float enemyRotateSpeed = 0.01f;

    private Text gameover;

    void Update()
    {
        if (GameObject.FindWithTag("Player")){
            Vector3 move = GameObject.FindWithTag("Player").transform.position - this.transform.position;
            if (move.x > 0)
                move.x = 1;
            else
                move.x = -1;
            if (move.z > 0)
                move.z = 1;
            else
                move.z = -1;
            transform.position += move * enemySpeed * Time.deltaTime;
            if (move != new Vector3(0, 0, 0))
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(move), enemyRotateSpeed);
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player"){
            GameOver();
            Destroy(col.gameObject);
        }
        if(col.gameObject.tag == "bullet"){
            
            Spawner enemynumber = FindObjectOfType<Spawner>();
            enemynumber.numberOfenemy--;
            FindObjectOfType<PlayerMovementControl>().score_count += 1;
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    private void GameOver()
    {
        gameover = GameObject.FindWithTag("GameOver").GetComponent<Text>();
        gameover.GetComponent<Text>().enabled = true;
    }
}
