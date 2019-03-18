using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour {

    private int pointsValue;
    private GameController manager;
	// Use this for initialization
	void Start ()
    {
        manager = GameObject.Find("GameController").GetComponent<GameController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            manager.addPoints(this.gameObject.tag);
            GameObject.FindObjectOfType<SceneChanger>().collectableSound();
            Destroy(this.gameObject);
        }
    }

}
