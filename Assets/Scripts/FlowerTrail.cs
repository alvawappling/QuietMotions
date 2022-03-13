using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTrail : MonoBehaviour{

    private float timeBtwSpawns;
    [SerializeField] private float startTimeBtwSpawns;

    [SerializeField] private GameObject[] flowerList;

    private float currentPosY;
    private float currentPosX;
    private float scaleFactor;

    private GameObject echo;


    void Update(){

        if(timeBtwSpawns <= 0){
            echo = flowerList[Random.Range(0, flowerList.Length)];


            currentPosY = transform.position.y + Random.Range(-0.5f, 0.5f);
            currentPosX = transform.position.x + Random.Range(-0.5f, 0.5f);

            GameObject flower = Instantiate(echo, new Vector3 (currentPosX, currentPosY, 0), Quaternion.identity);


            scaleFactor = Random.Range(0.0375f, 0.075f);

            Vector3 randomSize = new Vector3 (1,1,1) * scaleFactor;

            flower.transform.localScale = randomSize;



            timeBtwSpawns = startTimeBtwSpawns * Random.Range(0.2f, 0.8f);

            //timeBtwSpawns = startTimeBtwSpawns;

        } else {
            timeBtwSpawns -= Time.deltaTime;
        }
        
    }
}
