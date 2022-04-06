using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool HasEnded = false;
    private Vector3 camTargetPos = new Vector3(0, 30, -10);
    private Vector3 camCurrentPos;
    private bool gotPosition;
    private float targetSize = 36;
    private float currentSize;
    [SerializeField] private GameObject resetBtn;

    private void Start()
    {
        HasEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasEnded)
        {
            if (!gotPosition)
            {
                camCurrentPos = Camera.main.transform.position;
                currentSize = Camera.main.GetComponent<Camera>().orthographicSize;
                resetBtn.SetActive(true);
                gotPosition = true;
            }
            camCurrentPos = Vector3.Lerp(camCurrentPos, camTargetPos, 0.01f);
            Camera.main.transform.position = camCurrentPos;
            currentSize = Mathf.Lerp(currentSize, targetSize, 0.005f);
            Camera.main.GetComponent<Camera>().orthographicSize = currentSize;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
