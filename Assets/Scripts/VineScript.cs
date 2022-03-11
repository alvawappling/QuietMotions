using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float rotationSpeed, speed;
    public KeyCode toTheRight, toTheLeft;
    private int targetInput = 0;
    private float currentInput;
    public bool controlsAssigned = false;
    private bool stopped;
    private VineManager VM;

    // Start is called before the first frame update
    void Start()
    {
        VM = FindObjectOfType<VineManager>();
        currentInput = targetInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopped)
            return;

        if (Input.GetKey(toTheRight) && toTheRight != KeyCode.None)
            targetInput = 1;
        else if (Input.GetKey(toTheLeft) && toTheLeft != KeyCode.None)
            targetInput = -1;
        else
        {
            if (targetInput != 0)
                targetInput = 0;
        }

        currentInput = Mathf.Lerp(currentInput, targetInput, lerpSpeed);
        transform.Rotate(Vector3.forward * -currentInput * Time.deltaTime * rotationSpeed);

        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    public void AssignControls(KeyCode key)
    {
        if (toTheLeft == KeyCode.None)
            toTheLeft = key;
        else
        {
            toTheRight = key;
            controlsAssigned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Screen") && targetInput == 0)
        {
            stopped = true;
            VM.RemoveFromList(this);
        }
    }
}
