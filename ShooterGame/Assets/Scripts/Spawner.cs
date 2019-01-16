using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 1f;
    public GameObject enemy;
    public int numberOfenemy = 0;
    public float boundaryN;
    public float boundaryS;
    public float boundaryE;
    public float boundaryW;
    
    BoxCollider m_collider;
    // Start is called before the first frame update
    void Start()
    {
        boundaryN = GameObject.FindWithTag("boundaryN").transform.position.z - GameObject.FindWithTag("boundaryN").transform.localScale.z;
        boundaryS = GameObject.FindWithTag("boundaryS").transform.position.z + GameObject.FindWithTag("boundaryN").transform.localScale.z;
        boundaryE = GameObject.FindWithTag("boundaryE").transform.position.x - GameObject.FindWithTag("boundaryN").transform.localScale.z;
        boundaryW = GameObject.FindWithTag("boundaryW").transform.position.x + GameObject.FindWithTag("boundaryN").transform.localScale.z;

        InvokeRepeating("Spawn", spawnTime, spawnTime);

    }

    void Spawn()
    {
        if (GameObject.FindWithTag("Player"))
        {

            if (numberOfenemy > 10) {
                return;
            }
            Vector3 spawn_point = new Vector3(Random.Range(10, 21) * Random.Range(-1,1), 3, Random.Range(10, 21) * Random.Range(-1, 1));
            if (spawn_point.x == 0) spawn_point.x = Random.Range(10, 21);
            if (spawn_point.z == 0) spawn_point.z = Random.Range(10, 21);

            spawn_point += GameObject.FindWithTag("Player").transform.position;
           

            if (spawn_point.x > boundaryE){
                spawn_point.x = boundaryW;
            }
            else if (spawn_point.x < boundaryW){
                spawn_point.x = boundaryE;
            }
            if (spawn_point.z > boundaryN){
                spawn_point.z = boundaryS;
            }
            else if (spawn_point.z < boundaryS){
                spawn_point.z = boundaryN;
            }

            GameObject clone = Instantiate(enemy, spawn_point, new Quaternion(0, 0, 0, 0));
            numberOfenemy++;
            clone.AddComponent<Rigidbody>();
            clone.AddComponent<BoxCollider>();
            clone.AddComponent<EnemyBehaviour>();

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.useGravity = true;
            rb.detectCollisions = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            m_collider = clone.GetComponent<BoxCollider>();
            m_collider.center += new Vector3(0, 1, 0);
            m_collider.size = new Vector3(1,2.4f,1);

        }

    }
}   