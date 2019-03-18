using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifterZone : MonoBehaviour {

    private bool lifting = false;
    public GameObject platform;
    public float upwardConstraint;
	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //other.gameObject.GetComponent<Transform>().Translate((Vector3.up * Time.deltaTime), Space.Self);
            other.transform.parent = platform.transform;
            platform.GetComponent<Transform>().Translate((Vector3.up * Time.deltaTime), Space.Self);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            other.transform.parent = null;

        }
    }


    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Lift()
    {
        while(lifting)
        {
            
            yield return new WaitForSeconds(0);
        }
    }
}
