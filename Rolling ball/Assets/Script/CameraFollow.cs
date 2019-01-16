using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float rotation = 10;
    public float distance = 4;
    public float scroolSensitivity = 20;
    private float angle = 90;
    void Start()
    {
        this.transform.position = GameObject.FindWithTag("Player").transform.position - new Vector3(distance,-1,0);
        this.transform.LookAt(GameObject.FindWithTag("Player").transform.position);

    }

    void LateUpdate()
    {
        Vector3 playerpos = GameObject.FindWithTag("Player").transform.position;
        Vector3 camerapos = new Vector3(distance, 0, 0);
        
        if (Input.GetMouseButton(1) && Input.GetAxis("Mouse X") != 0) {
                

                angle -= Input.GetAxisRaw("Mouse X") * Time.deltaTime * 5;
                this.transform.position = playerpos + new Vector3(distance - distance * Mathf.Cos(angle), 1, Mathf.Sin(angle) * distance) - camerapos;
                
                this.transform.LookAt(playerpos);
        }
        else {
            this.transform.position = playerpos + new Vector3(distance - distance * Mathf.Cos(angle), 1, Mathf.Sin(angle) * distance) - camerapos; 
            this.transform.LookAt(playerpos);
        }
        if (Input.GetAxis("Mouse ScrollWheel")!=0){
            float view = Camera.main.fieldOfView;
            view = Mathf.Clamp(view, 40, 60);
            view += Input.GetAxis("Mouse ScrollWheel") * scroolSensitivity;
            Camera.main.fieldOfView = view;
        }
        
    }
}
