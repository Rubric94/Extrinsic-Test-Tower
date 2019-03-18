using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {

    public string actionString;
    private bool hasCollided = false;
    private string labelText = "";

    public Material newMat;
    private Material[] mats;
    private Renderer renderer;

    private void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        mats = renderer.materials;
    }

    void OnGUI ()
    {
		if(hasCollided == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 40), (labelText));

        }
	}

    // Update is called once per frame
    void Update()
    {
        if (hasCollided == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(AfterText());
                //this.gameObject.GetComponent<InteractScript>().enabled = false;


            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            hasCollided = true;
            labelText = "Press 'E' to\n" + actionString;

        }
    }

    void OnTriggerExit(Collider collider)
    {
        hasCollided = false;
    }

    IEnumerator AfterText()
    {
        if(this.gameObject.name == "Button")
        {
            mats[2] = newMat;
            renderer.materials = mats;
            GameObject.FindObjectOfType<GameController>().plusButton();
        }

        if (this.gameObject.name == "Golden_Gem")
        {
            renderer.enabled = false;
        }

        if (this.gameObject.name == "Chest")
        {
            
        }

        if (this.gameObject.name == "Pillar")
        {
            mats[1] = newMat;
            renderer.materials = mats;
            GameObject.FindObjectOfType<GameController>().plusPillar();
        }
        yield return new WaitForSeconds(.1f);
        this.gameObject.GetComponent<InteractScript>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
    }
}

