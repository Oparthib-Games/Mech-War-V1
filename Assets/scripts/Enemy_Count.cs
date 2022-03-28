using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Count : MonoBehaviour
{
    public float LevelLifeSpan;

    int which_level_to_unlock;

    public static int enemy_destroyed;
    public int score = 5;

    void Start()
    {
        which_level_to_unlock = SceneManager.GetActiveScene().buildIndex + 1;

        enemy_destroyed = 0;

        print(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        //print(enemy_destroyed);

        if (enemy_destroyed >= score)
        {
            if (PlayerPrefs.GetInt("level_reached") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("level_reached", which_level_to_unlock);
            }
            FindObjectOfType<Menu_script>().GetComponent<Menu_script>().Winning();
        }
    }
}
