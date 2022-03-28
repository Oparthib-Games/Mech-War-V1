using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class subscriberApi : MonoBehaviour
{
    public GameObject succescfull_panel;
    public GameObject reg_failed_panel;
    public GameObject internetCheckPanel;
    public GameObject inputField;
    string myCode;
    int is_success=0;
    

    public static DateTime realTime;

    // Start is called before the first frame update

        public void OpenURL()
        {
            Application.OpenURL("tel://*213*1344%23");
            Debug.Log("is this working?");
        }

    // void Start()
    //{
    //    StartCoroutine(Upload());
    //}

    public void Submit()
    {
        StartCoroutine(Upload());
        Debug.Log(myCode);
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("code", myCode);

        UnityWebRequest www = UnityWebRequest.Post("http://103.108.140.215/bdapps/subscriptionapp/api.php?appid=APP_021022", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);

            if (www.downloadHandler.text == "REGISTERED")
            {
                succescfull_panel.active = true;
                yield return new WaitForSeconds(3f);

                PlayerPrefs.SetInt("date_of_subscribe", DateTime.Now.Day);
                print("==>>>" + PlayerPrefs.GetInt("date_of_subscripton"));
                PlayerPrefs.SetInt("isSuccess", 1);
            }
            else
            {
                reg_failed_panel.active = true;
            }

        }

//123456 = REGISTERED
//112233 = UNREGISTERED
//445566 = PENDING
//Other code = Wrong Code

        // Or retrieve results as binary data
        //byte[] results = www.downloadHandler.data;

        //success->paly+timer start
        //notSuccess->code vul hole || resposne na ashle

        //if(www.downloadHandler.text == "user exist")
        //{
        //    PlayerPrefs.SetInt("isSuccess", 1);
        //    Application.LoadLevel("Levels");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("isSuccess", 0);
        //    //error msg dekhabe
        //}

    }

     void Update()
    {
        //print("==>>>" + PlayerPrefs.GetInt("date_of_subscripton"));
        //print("==>>>" + PlayerPrefs.GetInt("date_of_subscripton"));
        //Debug.LogError("=====>>>" + DateTime.Now);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            internetCheckPanel.active = true;
        }
        else
        {
            internetCheckPanel.active = false;
        }

        myCode = inputField.GetComponent<InputField>().text.ToString();

        if(PlayerPrefs.GetInt("date_of_subscribe") != DateTime.Now.Day)
        {
            PlayerPrefs.SetInt("isSuccess", 0);
        }

        if(PlayerPrefs.GetInt("isSuccess") == 1)
        {
            Application.LoadLevel("Level_selector");
        }

        //if (PlayerPrefs.GetInt("isSuccess") == 1)
        //{
        //    realTime = DateTime.Now;
        //    Application.LoadLevel("Levels");
        //}
        //else
        //{
        //    //show error msg.
        //}
    }




}
