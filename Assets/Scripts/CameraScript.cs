using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private List<GameObject> vinesAbove = new List<GameObject>();
    private List<GameObject> vinesBelow = new List<GameObject>();

    public GameObject currentVine;
    private Vector3 targetPos, currentPos;

    [SerializeField] private float minX, maxX, minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = targetPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.HasEnded)
            return;

        Vector3 targetPos = new Vector3(currentVine.transform.position.x, currentVine.transform.position.y, transform.position.z);
        if(targetPos.y > minY && targetPos.y < maxY)
            currentPos.y = Mathf.Lerp(currentPos.y, targetPos.y, 0.03f);

        if(targetPos.x > minX && targetPos.x < maxX)
            currentPos.x = Mathf.Lerp(currentPos.x, targetPos.x, 0.03f);

        transform.position = currentPos;
    }
}
