using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class QuestionaireManager : MonoBehaviour {

    private Scrollbar Enjoyment;
    private Dropdown Difficulty;
    private InputField Comments;
    private SceneChanger changer;

    private int id;
	// Use this for initialization
	void Start ()
    {
        Enjoyment = GameObject.FindObjectOfType<Scrollbar>();
        Difficulty = GameObject.FindObjectOfType<Dropdown>();
        Comments = GameObject.FindObjectOfType<InputField>();
        changer = GameObject.FindObjectOfType<SceneChanger>();

        if(SceneManager.GetActiveScene().name == "Questionnaire1")
        {
            id = 1;
        }
        if (SceneManager.GetActiveScene().name == "Questionnaire2")
        {
            id = 2;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void writeResults()
    {
        
        string path = Path.Combine(Application.streamingAssetsPath ,$"Test_Results_Lvl{id}.txt");
        using (var stream = new FileStream(path, FileMode.Append))
        {
            using (var writer = new StreamWriter(stream))
            {
                //STATS GO HERE!

                writer.WriteLine("////////////////////////////");
                writer.WriteLine("QUESTIONNAIRE RESULTS");
                writer.WriteLine($"Enjoyment Value : {(Enjoyment.value-0.5)*100}");
                writer.WriteLine($"Difficulty : {Difficulty.value} = {Difficulty.options[Difficulty.value].text}");
                writer.WriteLine($"Comment : {Comments.text}");

            }
        }

    }
}
