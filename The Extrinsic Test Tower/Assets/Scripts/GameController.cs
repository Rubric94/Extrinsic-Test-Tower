using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Achievement
{
    public string name = "";
    public string overtext = "";
    public bool achieved = false;
    public Sprite icon;
    public int pointValue = 0;
}

public class GameController : MonoBehaviour {

    public enum gameType {CollectablesAndPoints = 0, CollectablesAndAchievements = 1, PointsAndAchievements = 2 };
    public gameType build;

    float timer = 0;
    string minutes = "";
    string seconds = "";

    private string LVL1Time;
    private string LVL2Time;
    private string LVL3Time;
    private string LVL4Time;

    private int totalScore = 0;

    private int ButtonCount = 0;
    private int pillarsActivated = 0;
    private bool wallPassedThrough = false;

    public GameObject[] CollectableContainers;

    public GameObject[] buttons;
    public GameObject[] pillars;
    public GameObject[] fakeWalls;
    public GameObject Chest;
    public GameObject Gem;

    public Achievement[] achievements;
    private GameObject achievementDisplay;
    private GameObject achievementText;
    private GameObject pointsDisplay;
    private GameObject timeDisplay;

    private SceneChanger changer;
    private string path;

    int[] CollectableCount = new int[] { 0, 0, 0, 0 };
    int[] CollectableTotal;

    private bool paused = false;
    private bool gameRunning = false;
    // Use this for initialization
    void Start ()
    {
        achievementDisplay = GameObject.FindGameObjectWithTag("AchievementDisplay");
        pointsDisplay = GameObject.FindGameObjectWithTag("PointsDisplay");
        timeDisplay = GameObject.FindGameObjectWithTag("Timer");
        achievementText = GameObject.Find("AchievementText");

        changer = GameObject.FindObjectOfType<SceneChanger>();

        achievementDisplay.SetActive(false);
        achievementText.SetActive(false);

        if (build == gameType.CollectablesAndAchievements)
        {
            
            countCollectables();
            CollectableTotal = (int[])CollectableCount.Clone();

            achievements = new Achievement[8];
            for(int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = new Achievement();
            }
            
            achievements[0].name = "All Green";
            achievements[0].overtext = "Find all the green collectables in the game.";
            achievements[0].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_Green");

            achievements[1].name = "All Blue";
            achievements[1].overtext = "Find all the blue collectables in the game.";
            achievements[1].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_Blue");

            achievements[2].name = "All Red";
            achievements[2].overtext = "Find all the red collectables in the game.";
            achievements[2].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_Red");

            achievements[3].name = "Master Gatherer";
            achievements[3].overtext = "Find every collectable.";
            achievements[3].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_All");

            achievements[4].name = "Floor 1 Cleaner";
            achievements[4].overtext = "Find all the collectables on the 1st floor.";
            achievements[4].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_1");

            achievements[5].name = "Floor 2 Cleaner";
            achievements[5].overtext = "Find all the collectables on the 2nd floor.";
            achievements[5].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_2");

            achievements[6].name = "Floor 3 Cleaner";
            achievements[6].overtext = "Find all the collectables on the 3rd floor.";
            achievements[6].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_3");

            achievements[7].name = "Floor 4 Cleaner";
            achievements[7].overtext = "Find all the collectables on the 4th floor.";
            achievements[7].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Collectable_4");


            for (int i = 0; i < achievementDisplay.transform.childCount; i++)
            {
                achievementDisplay.transform.GetChild(i).GetComponent<Image>().sprite = achievements[i].icon;
                achievementDisplay.transform.GetChild(i).GetComponent<Button>().interactable = achievements[i].achieved;
            }


        }
        if(build != gameType.CollectablesAndAchievements && build != gameType.CollectablesAndPoints)
        {
            foreach(GameObject container in CollectableContainers)
            {
                container.SetActive(false);
            }
        }


        if(build == gameType.PointsAndAchievements)
        {
            foreach (GameObject wall in fakeWalls)
            {
                bool wallState = wall.GetComponent<Collider>().isTrigger;
                wall.GetComponent<Collider>().isTrigger = !wallState;
            }

            pointsDisplay.SetActive(true);

            achievements = new Achievement[8];
            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = new Achievement();
            }

            achievements[0].name = "Scrimper";
            achievements[0].overtext = "";
            achievements[0].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Points_1");
            achievements[0].pointValue = 500;

            achievements[1].name = "Wealthy";
            achievements[1].overtext = "";
            achievements[1].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Points_2");
            achievements[1].pointValue = 500;

            achievements[2].name = "Millionaire";
            achievements[2].overtext = "";
            achievements[2].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Points_3");
            achievements[2].pointValue = 1000;

            achievements[3].name = "Force of Habit";
            achievements[3].overtext = "Find and press all the buttons on the 1st floor.";
            achievements[3].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Buttons");
            achievements[3].pointValue = 5000;

            achievements[4].name = "Loot Finder";
            achievements[4].overtext = "Find the hidden chest.";
            achievements[4].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Chest");
            achievements[4].pointValue = 7000;

            achievements[5].name = "Jewel Thief";
            achievements[5].overtext = "Steal a large gem.";
            achievements[5].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Gem");
            achievements[5].pointValue = 8000;

            achievements[6].name = "Light the Way";
            achievements[6].overtext = "Activate all the pillars.";
            achievements[6].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Pillars");
            achievements[6].pointValue = 8000;

            achievements[7].name = "Shortcut";
            achievements[7].overtext = "Find a hidden shortcut on the 2nd floor.";
            achievements[7].icon = Resources.Load<Sprite>("AchievementIcons/Achievement_Wall");
            achievements[7].pointValue = 10000;

            for (int i = 0; i < achievementDisplay.transform.childCount; i++)
            {
                achievementDisplay.transform.GetChild(i).GetComponent<Image>().sprite = achievements[i].icon;
                achievementDisplay.transform.GetChild(i).GetComponent<Button>().interactable = achievements[i].achieved;
            }
        }
        else
        {
            foreach(GameObject button in buttons)
            {
                bool buttonState = button.activeSelf;
                button.SetActive(!buttonState);
            }

            foreach (GameObject pillar in pillars)
            {
                bool pillarState = pillar.activeSelf;
                pillar.SetActive(!pillarState);
            }

            Gem.SetActive(false);

            
        }

        if(build == gameType.CollectablesAndPoints)
        {
            countCollectables();
            CollectableTotal = (int[])CollectableCount.Clone();
        }
        
	}

	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2")
        {
            gameRunning = true;
        }

        if(gameRunning)
        {
            Timer();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(CollectableContainers[0].transform.childCount);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            paused = togglePause();
        }

        if (build == gameType.CollectablesAndPoints || build == gameType.PointsAndAchievements)
        {
            
            pointsDisplay.SetActive(true);
            pointsDisplay.GetComponentInChildren<Text>().text = $"Points : {totalScore}";

        }
        else
        {
            pointsDisplay.SetActive(false);
        }

        if(build != gameType.CollectablesAndAchievements && build != gameType.PointsAndAchievements)
        {
            achievementDisplay.SetActive(false);
            achievementText.SetActive(false);

        }
        
        if(build == gameType.CollectablesAndAchievements || build == gameType.CollectablesAndPoints)
        {
            tallyCollectables();
        }

        if(build == gameType.CollectablesAndAchievements)
        {
            if(GameObject.FindGameObjectsWithTag("Green").Length == 0 && !achievements[0].achieved)
            {
                changer.achievementSound();
                achievements[0].achieved = true;
            }

            if (GameObject.FindGameObjectsWithTag("Blue").Length == 0 && !achievements[1].achieved)
            {
                changer.achievementSound();
                achievements[1].achieved = true;
            }

            if (GameObject.FindGameObjectsWithTag("Red").Length == 0 && !achievements[2].achieved)
            {
                changer.achievementSound();
                achievements[2].achieved = true;
            }

            if (GameObject.FindGameObjectsWithTag("Green").Length == 0 && GameObject.FindGameObjectsWithTag("Blue").Length == 0 && GameObject.FindGameObjectsWithTag("Red").Length == 0 && !achievements[3].achieved)
            {
                changer.achievementSound();
                achievements[3].achieved = true;
            }

            if(CollectableContainers[0].transform.childCount == 0 && !achievements[4].achieved)
            {
                changer.achievementSound();
                achievements[4].achieved = true;
            }

            if (CollectableContainers[1].transform.childCount == 0 && !achievements[5].achieved)
            {
                changer.achievementSound();
                achievements[5].achieved = true;
            }

            if (CollectableContainers[2].transform.childCount == 0 && !achievements[6].achieved)
            {
                changer.achievementSound();
                achievements[6].achieved = true;
            }

            if (CollectableContainers[3].transform.childCount == 0 && !achievements[7].achieved)
            {
                changer.achievementSound();
                achievements[7].achieved = true;
            }

            for (int i = 0; i < achievementDisplay.transform.childCount; i++)
            {
                achievementDisplay.transform.GetChild(i).GetComponent<Button>().interactable = achievements[i].achieved;
            }
        }

        if(build == gameType.PointsAndAchievements)
        {
            if(totalScore >= 6000 && !achievements[0].achieved)
            {
                changer.achievementSound();
                achievements[0].achieved = true;
                addPoints(achievements[0]);
            }

            if (totalScore >= 16000 && !achievements[1].achieved)
            {
                changer.achievementSound();
                achievements[1].achieved = true;
                addPoints(achievements[1]);
            }

            if (totalScore >= 38000 && !achievements[2].achieved)
            {
                changer.achievementSound();
                achievements[2].achieved = true;
                addPoints(achievements[2]);
            }
            if (ButtonCount == buttons.Length && !achievements[3].achieved)
            {
                changer.achievementSound();
                achievements[3].achieved = true;
                addPoints(achievements[3]);
            }

            if(Chest.GetComponent<InteractScript>().enabled == false && !achievements[4].achieved)
            {
                changer.achievementSound();
                achievements[4].achieved = true;
                addPoints(achievements[4]);
            }

            if (Gem.GetComponent<InteractScript>().enabled == false && !achievements[5].achieved)
            {
                changer.achievementSound();
                achievements[5].achieved = true;
                addPoints(achievements[5]);
            }

            if (pillarsActivated == pillars.Length && !achievements[6].achieved)
            {
                changer.achievementSound();
                achievements[6].achieved = true;
                addPoints(achievements[6]);
            }

            if (wallPassedThrough && !achievements[7].achieved)
            {
                changer.achievementSound();
                achievements[7].achieved = true;
                addPoints(achievements[7]);

            }

            for (int i = 0; i < achievementDisplay.transform.childCount; i++)
            {
                achievementDisplay.transform.GetChild(i).GetComponent<Button>().interactable = achievements[i].achieved;
            }

            //FILL IN ACHIEVEMENTS FOR POINTS!
        }

        timeDisplay.GetComponentInChildren<Text>().text = $"Time : {minutes}:{seconds}";
    }

    public string getAchievementDescription(int id)
    {
        return achievements[id].overtext;
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        minutes = Mathf.Floor(timer / 60).ToString("00");
        seconds = (timer % 60).ToString("00");
        
    }

    public void plusButton()
    {
        ButtonCount++;
    }

    public void plusPillar()
    {
        pillarsActivated++;
    }

    public void addPoints(string colour)
    {
        if (colour == "Green")
        {
            totalScore += 500;
        }

        if (colour == "Blue")
        {
            totalScore += 1000;
        }

        if (colour == "Red")
        {
            totalScore += 10000;
        }
    }

    public void addPoints(Achievement achievement)
    {
        totalScore += achievement.pointValue;
    }

   

    public int getScore()
    {
        return totalScore;
    }
    
    public string getTime()
    {
        return timer.ToString("F2");
    }

    private void countCollectables()
    {
        for (int i = 0; i < CollectableContainers.Length; i++)
        {
            CollectableCount[i] = CollectableContainers[i].transform.childCount;
        }
    }

    private void tallyCollectables()
    {
        for(int i = 0; i<CollectableContainers.Length; i++)
            {
                CollectableCount[i] = CollectableContainers[i].transform.childCount;
            }
    }

    public void addScore(int score)
    {
        totalScore += score;
    }

    public void writeResults()
    {
        
        if(SceneManager.GetActiveScene().name == "Level1")
        {
            path = Path.Combine(Application.streamingAssetsPath, "Test_Results_Lvl1.txt");

        }
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            path = Path.Combine(Application.streamingAssetsPath, "Test_Results_Lvl2.txt");
        }

        using (var stream = new FileStream(path, FileMode.Truncate))
        {
            using (var writer = new StreamWriter(stream))
            {
                //STATS GO HERE!
                writer.WriteLine($"Build : {build}");
                //writer.WriteLine($"Level 1 Time : {minutes}:{seconds}");
                //writer.WriteLine($"Level 2 Time : {minutes}:{seconds}");
                //writer.WriteLine($"Level 3 Time : {minutes}:{seconds}");
                //writer.WriteLine($"Level 4 Time : {minutes}:{seconds}");
                writer.WriteLine($"Time : {minutes}:{seconds}");

                if (build == gameType.PointsAndAchievements || build == gameType.CollectablesAndPoints)
                {
                    writer.WriteLine($"Points : {totalScore}");
                }

                if (build == gameType.CollectablesAndAchievements || build == gameType.CollectablesAndPoints)
                {
                    writer.WriteLine("/////Collectables/////");
                    for (int i = 0; i < CollectableContainers.Length; i++)
                    {
                        writer.WriteLine($"Level {i} Collectables : {CollectableCount[i]}/{CollectableTotal[i]}");
                    }
                }

                if(build == gameType.CollectablesAndAchievements || build == gameType.PointsAndAchievements)
                {
                    writer.WriteLine("/////Achievements/////");
                    foreach (Achievement achievement in achievements)
                    {
                        writer.WriteLine($"{achievement.name} = {achievement.achieved}");
                    }
                }
                
               

            }
        }

    }

    public void DoActivateTrigger()
    {
        wallPassedThrough = true;
    }

    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
            if(build == gameType.CollectablesAndAchievements || build == gameType.PointsAndAchievements)
            {
                achievementDisplay.SetActive(false);
                achievementText.SetActive(false);
            }
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
            if (build == gameType.CollectablesAndAchievements || build == gameType.PointsAndAchievements)
            {
                achievementDisplay.SetActive(true);
                achievementText.SetActive(true);
            }
            return (true);
        }
    }

}
