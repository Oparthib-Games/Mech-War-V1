using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class level_selector_script : MonoBehaviour
{
    public Material red_mat;
    public Material yellow_mat;
    public Material black_mat;
    public Material green_mat;

    Vector3 myPos;
    Vector3 response_pos;

    int unlocked_level;
    public int which_level_to_load;

    void Start()
    {
        myPos = transform.position;
        response_pos = transform.position + new Vector3(0, 1.5f, 0);

        red_mat = GetComponent<Renderer>().material;

        unlocked_level = PlayerPrefs.GetInt("level_reached", 1);
    }


    void Update()
    {
        if(unlocked_level < which_level_to_load)
        {
            transform.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            transform.GetComponentInChildren<Text>().color = Color.white;
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                PlayerPrefs.DeleteAll();
                Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);

            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayerPrefs.SetInt("level_reached", 20);
                Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }



    /********************************************************************************************************************/

    void OnMouseUp()
    {
        if (!Panning.isPanning)
        {
            if (unlocked_level >= which_level_to_load)
            {
                Application.LoadLevel(which_level_to_load);
            }
        }
    }

    void OnMouseOver()
    {
        if (unlocked_level >= which_level_to_load)
        {
            transform.position = Vector3.Lerp(response_pos, myPos, 0.0001f);
            GetComponent<Renderer>().material = yellow_mat;
        }
        else
        {
            transform.position = Vector3.Lerp(response_pos, myPos, 0.0001f);
            GetComponent<Renderer>().material = black_mat;
        }
        
    }
    void OnMouseExit()
    {
        transform.position = Vector3.Lerp(myPos, response_pos, 0.0001f);
        GetComponent<Renderer>().material = red_mat;
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void Unlock()
    {
        PlayerPrefs.SetInt("level_reached", 20);
        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
