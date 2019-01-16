using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private bool grounded = true;

    public Text stext;
    public Text wtext;
    private int score=0;

    private Rigidbody rb;

    public Camera camera;

    public float speed = 5;
    public float jump = 200;

    public float speeder = 10;
    public float maxspeed = 10;
    public float speed_multiplier = 1.2f;
    public float death_thresholder = -2;
    private float value;
    
    private bool move = true;
    private bool flag = true;

    Vector3 position = new Vector3(0, 0, 0);

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        value = maxspeed; 
    }

    void Update()
    {

        GameObject terrain = GameObject.FindWithTag("Terrain");
        Vector3 t_coordinates = terrain.GetComponent<Terrain>().terrainData.size;

        stext.text = "Score: " + score.ToString();

        if (move)
        { 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement(speed * 2);
                maxspeed = value * speed_multiplier;
            }
            else
            {
                maxspeed = value;
                movement(speed);
            }
            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                rb.AddForce(0,jump,0);
                grounded = false;
            }
        }
        if (this.transform.position.x < 0 || this.transform.position.z < 0 || this.transform.position.x > t_coordinates.x  || this.transform.position.z > t_coordinates.z)
        {
            if (flag)
            {
                position = this.transform.position;
                flag = false;
            }
            if (this.transform.position.y < death_thresholder)
            {
                teleport_nearest(t_coordinates,position);
            }
        }
    }

    private void teleport_nearest(Vector3 t_coordinates,Vector3 position)
    {
        Vector3 offset = new Vector3(0,0,0);
        
        if (this.transform.position.y<death_thresholder)
        {
            flag = true;
            rb.Sleep();
            StartCoroutine(Delay());
            if (transform.position.x<0)
            {
                offset += new Vector3(6, 0, 0);
            }
            else if (transform.position.x>t_coordinates.x)
            {
                offset += new Vector3(-6, 0, 0);
            }
            if (transform.position.z < 0)
            {
                offset += new Vector3(0, 0, 6);
            }
            else if (transform.position.z > t_coordinates.z)
            {
                offset += new Vector3(0, 0, -6);
            }
            this.transform.position = position + offset + new Vector3(0,10,0);
            
        }

        
    }

    IEnumerator Delay()
    {
        move = false;
        yield return new WaitForSeconds(2);
        move = true;
    }

    private void movement(float speed)
    {

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Camera.main.transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Camera.main.transform.forward * speed * -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Camera.main.transform.right * speed * -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Camera.main.transform.right * speed);
        }
        if (rb.velocity.magnitude > maxspeed)
        {

            rb.velocity -= rb.velocity.normalized * maxspeed * Time.deltaTime;
        }
        
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag=="Terrain")
        {
            grounded = true;
        }

        if (col.gameObject.tag=="Speeder"){
            rb.velocity = col.transform.forward * speeder * rb.velocity.magnitude;
        }
        if (col.gameObject.tag == "points")
        {
            score++;
        }
        if (col.gameObject.tag == "Finish")
        {
            wtext.GetComponent<Text>().enabled = true;

        }
    }
}
