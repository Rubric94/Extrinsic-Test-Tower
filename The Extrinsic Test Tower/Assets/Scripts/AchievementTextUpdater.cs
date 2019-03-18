using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTextUpdater : MonoBehaviour
{

    private GameController gameManager;
    private GameObject[] achievementImages;
    private Text textObject;
    private int id;
    // Use this for initialization
    void Start()
    {
        if(this.gameObject.name[12] == '0')
        {
            id = 0;
        }
        if (this.gameObject.name[12] == '1')
        {
            id = 1;
        }
        if (this.gameObject.name[12] == '2')
        {
            id = 2;
        }
        if (this.gameObject.name[12] == '3')
        {
            id = 3;
        }
        if (this.gameObject.name[12] == '4')
        {
            id = 4;
        }
        if (this.gameObject.name[12] == '5')
        {
            id = 5;
        }
        if (this.gameObject.name[12] == '6')
        {
            id = 6;
        }
        if (this.gameObject.name[12] == '7')
        {
            id = 7;
        }
        textObject = GameObject.Find("AchievementText").GetComponentInChildren<Text>();
        gameManager = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateText()
    {
        textObject.text = gameManager.getAchievementDescription(id);
    }

    public void clearText()
    {
        textObject.text = "";
    }
}
