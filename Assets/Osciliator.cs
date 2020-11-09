using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Osciliator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(20f,0f,0f);
    [Range (0,1) ] [SerializeField] float movementFactor;
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movementFactor = Mathf.Sin(Time.time*Mathf.PI)/2 + 0.5f; //ranging from -1 to 1 divied by 2 then add 0.5 to make it ranging from 0 to 1
        transform.position = startingPosition + movementVector * movementFactor;
    }
}
