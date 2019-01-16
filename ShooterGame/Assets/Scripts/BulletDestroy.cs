using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    float boundaryN;
    float boundaryS;
    float boundaryE;
    float boundaryW;
    void Start()
    {
        boundaryN = FindObjectOfType<Spawner>().boundaryN;
        boundaryS = FindObjectOfType<Spawner>().boundaryS;
        boundaryE = FindObjectOfType<Spawner>().boundaryE;
        boundaryW = FindObjectOfType<Spawner>().boundaryW;


    }
    void Update()
    {
        if (this.transform.position.x> boundaryN+10 || this.transform.position.x<boundaryS-10 || 
            this.transform.position.z>boundaryE+10 || this.transform.position.z<boundaryW-10){
            Destroy(this.gameObject);
        }   
    }
    void OnCollisionEnter(Collision col)
    {
        if (GameObject.FindWithTag("terrain") == col.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}
