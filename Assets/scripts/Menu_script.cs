using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_script : MonoBehaviour
{
    public static bool isPaused;
    bool isGameOver;

    public GameObject normalCanvas;
    public GameObject pausePanel;
    public GameObject lose_panel;
    public GameObject win_panel;

    public float timeSCAL = 1;

    void Awake()
    {
        pausePanel.SetActive(false);
        lose_panel.SetActive(false);
        win_panel.SetActive(false);

        normalCanvas = FindObjectOfType<lose_manager>().gameObject;

        Time.timeScale = timeSCAL;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !isPaused && !isGameOver)
        {
            Pause();
        }
        else if (Input.GetKey(KeyCode.Escape) && isPaused && !isGameOver)
        {
            Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        normalCanvas.SetActive(false);
        Time.timeScale = 0f;
        //Time.fixedDeltaTime = 0f;
        pausePanel.SetActive(true);
        buttonCtrl.selected_gameobj = null;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = timeSCAL;
        //Time.fixedDeltaTime = normal_game_speed;
        normalCanvas.SetActive(true);
    }

    public void Winning()
    {
        //attack_mode[] all_attacker = FindObjectsOfType<attack_mode>();

        //foreach(attack_mode the_attacker in all_attacker)
        //{
        //    the_attacker.GetComponent<health_script>().dealDamage(2000);
        //}

        if(SceneManager.GetActiveScene().name == "scene_01")
        {
            if(PlayerPrefs.GetInt("isSuccess") == 0)
            {
                Application.LoadLevel("SubscriberScene");
                return;
            }
        }


        isPaused = true;
        isGameOver = true;

        normalCanvas.SetActive(false);
        pausePanel.SetActive(false);
        lose_panel.SetActive(false);

        buttonCtrl.selected_gameobj = null;
        win_panel.SetActive(true);
    }

    public void Losing()
    {
        common_defender_script[] all_defenders = FindObjectsOfType<common_defender_script>();

        foreach(common_defender_script the_defender in all_defenders)
        {
            the_defender.GetComponent<health_script>().dealDamage(2000);
        }

        isPaused = true;
        isGameOver = true;

        normalCanvas.SetActive(false);
        pausePanel.SetActive(false);
        win_panel.SetActive(false);

        //Time.timeScale = 0f;
        //Time.fixedDeltaTime = 0f;
        buttonCtrl.selected_gameobj = null;
        lose_panel.SetActive(true);
    }

    public void LoadLevel(string level_name)
    {
        Application.LoadLevel(level_name);
    }

    public void Quit()
    {
        Application.LoadLevel("Level_selector");
    }

    public void Reload()
    {
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
