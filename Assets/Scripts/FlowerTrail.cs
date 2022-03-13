using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTrail : MonoBehaviour{

    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echo;
        
    void Update(){

        if(timeBtwSpawns <= 0){
            // spawn echo game object
            Instantiate(echo, transform.position, Quaternion.identity);
            timeBtwSpawns = startTimeBtwSpawns;
        } else {
            timeBtwSpawns -= Time.deltaTime;
        }
        
    }
}
