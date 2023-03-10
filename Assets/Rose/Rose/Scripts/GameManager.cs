using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    //Variables
    //HUD
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;
    public Button optionsButton;

         //Stuff Danny Added
    public float currentHealth;
    public HealthBar healthbar;

    //General
    public bool isGameActive;
    public GameObject titleScreen;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("TitleScreen");

    }
    //Starts the Game
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        score = 0;
        UpdateScore(0);

            //Stuff Danny Added
        healthbar = GameObject.Find("healthBar").GetComponent<HealthBar>();
    }


    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }


    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {

        SceneManager.LoadScene("Title Screen");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }












    // Update is called once per frame
    void Update()
    {
         //Stuff Danny Added
         healthbar.health = currentHealth;

        if (Input.GetButtonDown("Start") && !isGameActive)
        {
            StartGame(1);
        }

        else if (Input.GetButtonDown("Restart") && !isGameActive)
        {
            RestartGame();
        }
        if (Input.GetButtonDown("Exit") && !isGameActive)
        {
            Application.Quit();
        }
    }

}
