using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitObject : MonoBehaviour
{  

    public int speed = 10;
    public bool invert = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(invert)
            transform.Rotate(0f, -speed*Time.deltaTime, -speed*Time.deltaTime, Space.Self);
        else transform.Rotate(0f, 0f, speed*Time.deltaTime, Space.Self);
    }
}
