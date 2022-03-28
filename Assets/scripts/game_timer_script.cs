using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_timer_script : MonoBehaviour
{
    Slider slider;

    public float LevelLifeSpan;

    int which_level_to_unlock;

    void Start()
    {
        slider = GetComponent<Slider>();

        which_level_to_unlock = SceneManager.GetActiveScene().buildIndex + 1;

        print(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        slider.value = Time.timeSinceLevelLoad / LevelLifeSpan;

        if(slider.value == 1)
        {
            if(PlayerPrefs.GetInt("level_reached") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("level_reached", which_level_to_unlock);
            }
            FindObjectOfType<Menu_script>().GetComponent<Menu_script>().Winning();
        }
    }
}
