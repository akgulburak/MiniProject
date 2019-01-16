using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public int distance = 20;
    public float speed = 5;

    private bool firstperson = false;

    public float mhorizantalSpeed = 5;
    public float mverticalSpeed = 5;

    private float rotate_x = 0f;
    private float rotate_y = 0f;
    
    public int minimum_y = -5;
    public int maximum_y = 5;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindWithTag("Player"))
        {
            GameObject player_obj = GameObject.FindWithTag("Player");
            GameObject camera = GameObject.FindWithTag("MainCamera");

            if (Input.GetKeyDown(KeyCode.X)) {
                if (firstperson){
                    camera.transform.rotation = Quaternion.Euler(90, 0, 0);
                }
                else {
                    camera.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                firstperson = !firstperson;
            }
            if (firstperson) {

                MouseRotate(camera,player_obj);
            }
            else{
                camera.transform.position = new Vector3(player_obj.transform.position.x, player_obj.transform.position.y + distance, player_obj.transform.position.z);
            }
        }
    }

    private void MouseRotate(GameObject camera,GameObject player_obj)
    {

        rotate_x += Input.GetAxis("Mouse Y");
        rotate_y += Input.GetAxis("Mouse X");

        rotate_x = Mathf.Clamp(rotate_x,minimum_y,maximum_y);

        camera.transform.position = new Vector3(player_obj.transform.position.x, player_obj.transform.position.y, player_obj.transform.position.z);

        camera.transform.localEulerAngles = new Vector3(rotate_x*mverticalSpeed*-1,rotate_y*mhorizantalSpeed,0);

        player_obj.transform.localEulerAngles = new Vector3(rotate_x * mverticalSpeed, rotate_y * mhorizantalSpeed, 0);
        if(Input.GetAxis("Vertical")>0)
            player_obj.transform.position += transform.forward * speed * Time.deltaTime;
    }
}
