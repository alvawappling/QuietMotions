using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private int positionOfZ;
    private int positionOfAlpha;
    [SerializeField] private TextMeshProUGUI text;


    void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        for(int i = 0; i < values.Length; i++)
        {
            if(values[i] == (int)KeyCode.Z)
            {
                positionOfZ = i;
                break;
            }
        }

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == (int)KeyCode.Alpha0)
            {
                positionOfAlpha = i;
                return;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camScript = FindObjectOfType<CameraScript>();
        currentVine = firstVine;
        camScript.currentVine = currentVine.gameObject;
        vines.Add(currentVine);
        ChangeText();
        StartCoroutine(BranchVine());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.HasEnded)
            return;

        if (Input.anyKeyDown)
        {
            AssignKey();
        }
    }

    private void AssignKey()
    {
        if (currentVine.controlsAssigned)
            return;

        KeyCode tempKey = KeyCode.None;
        for (int i = 0; i <= positionOfZ; i++)
        {
            bool keyExist = false;
            if (Input.GetKeyDown((KeyCode)values[i]) != true || Input.GetKeyDown(KeyCode.None))
                continue;

            foreach (KeyCode key in usedKeys)
            {
                if (key == (KeyCode)values[i])
                {
                    keyExist = true;
                    break;
                }
            }

            if (keyExist)
                continue;

            tempKey = (KeyCode)values[i];
            usedKeys.Add(tempKey);

            if (tempKey != KeyCode.None)
            {
                currentVine.AssignControls(tempKey);
                ChangeText();
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
            ChangeText();
            StartCoroutine(BranchVine());
        }
    }

    public void ChangeVine(VineScript outVine)
    {
        if(vines.Count - 1 == 0)
        {
            GameManager.HasEnded = true;
            text.GetComponent<Animator>().SetTrigger("GameEnd");
        }
        else if(outVine == currentVine)
        {
            int randomVine = -1;
            do
            {
                randomVine = Random.Range(0, vines.Count);
                currentVine = vines[randomVine];
                camScript.currentVine = currentVine.gameObject;
                ChangeText();
            }while (outVine == vines[randomVine] || !currentVine.controlsAssigned) ;

        }
    }

    public void RemoveFromList(VineScript vine, KeyCode left, KeyCode right)
    {
        for(int i = 0; i < vines.Count; i++)
        {
            if(vines[i] == vine)
            {
                vines.RemoveAt(i);
            }
        }

        for(int i = 0; i < usedKeys.Count; i++)
        {
            if (usedKeys[i] == left || usedKeys[i] == right)
                usedKeys.RemoveAt(i);
        }
    }

    private void ChangeText()
    {
        string left = "~";
        string right = "~";

        if (currentVine.toTheLeft != KeyCode.None)
            left = currentVine.toTheLeft.ToString();

        if (currentVine.toTheRight != KeyCode.None)
            right = currentVine.toTheRight.ToString();

        text.text = left + ", " + right;
    }
}
