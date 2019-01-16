using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementControl : MonoBehaviour
{
    public float speed = 60;
    public float rotate_speed = 1f;

    public GameObject bullet;
    public Transform bulletSpawn;

    private Rigidbody rb;

    private float nextFire = 0.0f;
    public float fireRate = 1f;
    public int bulletspeed = 1200;

    public Text score;
    public int score_count = 0;

    public bool FPflag = false;

    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            FPflag = !FPflag;
        }
        score.text = "Score: " + score_count.ToString();
        shots();
        face_movement();
    }


    private void shots()
    {
        if (Input.GetKey("space")==true && Time.time > nextFire){
            Rigidbody rb;

            nextFire = Time.time + fireRate;
            GameObject shot = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

            shot.gameObject.tag = "bullet";
            shot.AddComponent<BulletDestroy>();
            shot.AddComponent<Rigidbody>();
            rb = shot.GetComponent<Rigidbody>();
            shot.AddComponent<BoxCollider>();
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.AddForce(transform.forward*bulletspeed);
            rb.detectCollisions = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            
        }
    }

    public Vector3 movement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
        return move;
    }

    void face_movement()
    {
        if (!FPflag)
        {
            Vector3 vector = movement();
            if (vector != new Vector3(0, 0, 0)) {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(vector), rotate_speed);
            }
        }
    }
}
