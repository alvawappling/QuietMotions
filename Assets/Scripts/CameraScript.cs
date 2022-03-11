using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> vinesAbove = new List<GameObject>();
    private List<GameObject> vinesBelow = new List<GameObject>();

    public GameObject currentVine;
    private Vector3 targetPos, currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = targetPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(currentVine.transform.position.x, currentVine.transform.position.y, transform.position.z);
        currentPos = Vector3.Slerp(currentPos, targetPos, 0.1f);
        transform.position = currentPos;
    }
}
