using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_and_Bob : MonoBehaviour
{

    public bool spin = true;
    public float degreesPerSecond = 15.0f;

    public bool bob = true;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempOffset = new Vector3();

    // Use this for initialization
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
            Spin();
        if (bob)
            Bob();
    }

    void Spin()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
    }

    void Bob()
    {
        Vector3 tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
