using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonCtrl : MonoBehaviour
{
    public GameObject buttons_gameObj;

    public static GameObject selected_gameobj;
    public static int placing_cost;

    public int cost;

    Transform cost_panel;

    Color cost_text_idle_color;

    int power_amount;

    Text cost_text;

    void Start()
    {

        cost_panel = this.gameObject.transform.GetChild(0).transform.GetChild(0);

        //cost_panel = GetComponentInChildren<Transform>().GetComponentInChildren<Transform>();

        //cost_panel = this.transform.Find("cost");

        cost_text = cost_panel.GetComponent<Text>();


        cost_text_idle_color = cost_text.color;

        cost = buttons_gameObj.GetComponent<common_defender_script>().cost;

        cost_text.text = cost.ToString();
    }

    void Update()
    {
        power_amount = FindObjectOfType<power_display_script>().power;

        if(power_amount < cost)
        {
            cost_text.color = Color.red;
        }
        else
        {
            cost_text.color = cost_text_idle_color;
        }
    }

    //void OnMouseDown()
    //{
    //    selected_gameobj = buttons_gameObj;
    //    placing_cost = selected_gameobj.GetComponent<common_defender_script>().cost;
    //}

    public void obj_select()
    {
        selected_gameobj = buttons_gameObj;
        placing_cost = selected_gameobj.GetComponent<common_defender_script>().cost;
    }
}
