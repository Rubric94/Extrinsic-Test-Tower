using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class SceneChanger : MonoBehaviour {

    private string participantID;
    public AudioClip[] sounds;
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this.gameObject);
        participantID = UniqueID();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            GameObject.Find("IDDisplay").GetComponent<Text>().text = $"ID = {participantID}";
        }


        if(SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            GameObject.Find("Portal").GetComponent<ActivateTrigger>().target = this.gameObject;
        }

        if (SceneManager.GetActiveScene().name == "EndScreen")
        {
            GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(delegate { Quit(); });
        }


        if (SceneManager.GetActiveScene().name == "Questionnair1" || SceneManager.GetActiveScene().name == "Questionnair2" || SceneManager.GetActiveScene().name == "EndScreen")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


    }

    private static char GetLetter()
    {
        string chars = SystemInfo.deviceUniqueIdentifier;

        chars.ToCharArray();
        return chars[Random.Range(0, chars.Length - 1)];
    }

    public void DoActivateTrigger()
    { 
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            GameObject.FindObjectOfType<GameController>().writeResults();
            changeScene("Questionnair1");
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            GameObject.FindObjectOfType<GameController>().writeResults();
            changeScene("Questionnair2");
        }
    }

    private string UniqueID()
    {
        string generatedID = "";
        for (int i = 0; i < 4; i++)
        {
            generatedID = generatedID.Insert(i, GetLetter().ToString());
        }

        return generatedID;
    }

    public void changeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public string retrieveID()
    {
        return participantID;
    }

    public void collectableSound()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(sounds[0]);
    }

    public void achievementSound()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(sounds[1]);
    }
}
