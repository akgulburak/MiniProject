using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    void Update(){
        if(!GameObject.FindWithTag("Player") && Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
