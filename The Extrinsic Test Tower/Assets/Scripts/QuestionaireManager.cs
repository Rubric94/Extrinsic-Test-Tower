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

    private string path = "";
    private int id;
	// Use this for initialization
	void Start ()
    {
        Enjoyment = GameObject.FindObjectOfType<Scrollbar>();
        Difficulty = GameObject.FindObjectOfType<Dropdown>();
        Comments = GameObject.FindObjectOfType<InputField>();
        changer = GameObject.FindObjectOfType<SceneChanger>();
        GameObject.Find("next").GetComponent<Button>().onClick.AddListener(delegate { writeResults(); });
        if (SceneManager.GetActiveScene().name == "Questionnair1")
        {
            path = Path.Combine(Application.streamingAssetsPath, "Test_Results_Lvl1.txt");

        }
        if (SceneManager.GetActiveScene().name == "Questionnair2")
        {
            path = Path.Combine(Application.streamingAssetsPath, "Test_Results_Lvl2.txt");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void writeResults()
    {

        

        using (var stream = new FileStream(path, FileMode.Append))
        {
            GameObject.Find("next").GetComponent<Button>().enabled = false;
            using (var writer = new StreamWriter(stream))
            {
                //STATS GO HERE!

                writer.WriteLine("////////////////////////////");
                writer.WriteLine("QUESTIONNAIRE RESULTS");
                writer.WriteLine($"Enjoyment Value : {((Enjoyment.value-0.5)*100)*2}");
                writer.WriteLine($"Difficulty : {Difficulty.value} = {Difficulty.options[Difficulty.value].text}");
                if (Comments.text!=null)
                {
                    writer.WriteLine($"Comment : {Comments.text}");
                }
                else
                {
                    writer.WriteLine($"Comment : No Comment.");
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "Questionnair1")
        {
            GameObject.FindObjectOfType<SceneChanger>().changeScene("Level2");

        }
        if (SceneManager.GetActiveScene().name == "Questionnair2")
        {
            GameObject.FindObjectOfType<SceneChanger>().changeScene("EndScreen");
        }
        

    }
}
