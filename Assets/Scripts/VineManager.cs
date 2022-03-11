using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineManager : MonoBehaviour
{
    private int[] values;
    private List<KeyCode> usedKeys = new List<KeyCode>();
    private List<VineScript> vines = new List<VineScript>();
    public GameObject vinePrefab;
    public VineScript firstVine;
    private VineScript currentVine;
    [SerializeField] private float minTime, maxTime;
    private int spawnedVines = 1;
    private CameraScript camScript;


    void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
    }

    // Start is called before the first frame update
    void Start()
    {
        camScript = FindObjectOfType<CameraScript>();
        currentVine = firstVine;
        camScript.currentVine = currentVine.gameObject;
        vines.Add(currentVine);
        StartCoroutine(BranchVine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            AssignKey();
        }
    }

    private void AssignKey()
    {
        if (!currentVine.controlsAssigned)
        {
            KeyCode tempKey = KeyCode.None;
            for (int i = 0; i < values.Length; i++)
            {
                if (Input.GetKey((KeyCode)values[i]) == true)
                {
                    foreach (KeyCode key in usedKeys)
                    {
                        if (key == (KeyCode)values[i])
                            return;
                    }
                    tempKey = (KeyCode)values[i];
                    usedKeys.Add(tempKey);

                    if (tempKey != KeyCode.None)
                        currentVine.AssignControls(tempKey);
                }
            }
        }
    }

    private IEnumerator BranchVine()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);

        if (vines.Count != 0)
        {
            currentVine = vines[Random.Range(0, vines.Count)];
            Vector3 rightAngle = currentVine.transform.eulerAngles + new Vector3(0, 0, 45);
            Vector3 leftAngle = currentVine.transform.eulerAngles - new Vector3(0, 0, 45);

            GameObject newVine = Instantiate(vinePrefab, currentVine.transform.position, currentVine.transform.transform.rotation);
            newVine.transform.eulerAngles = leftAngle;
            currentVine.transform.eulerAngles = rightAngle;

            currentVine = newVine.GetComponent<VineScript>();
            vines.Add(currentVine);
            spawnedVines++;
            currentVine.name = "Vine_" + spawnedVines;
            camScript.currentVine = currentVine.gameObject;
            StartCoroutine(BranchVine());
        }
    }

    public void RemoveFromList(VineScript vine)
    {
        for(int i = 0; i < vines.Count; i++)
        {
            if(vines[i] == vine)
            {
                vines.RemoveAt(i);
            }
        }
    }
}
