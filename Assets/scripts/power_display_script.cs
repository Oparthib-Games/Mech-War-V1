using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class power_display_script : MonoBehaviour
{
    Text power_text;

    public int power;

    public enum placing_status_enum {S, F} // SUCCESS or FAILURE

    void Start()
    {
        power_text = GetComponent<Text>();
        Update_Power_Text();
    }

    public void Add_Power(int power_amount)
    {
        power += power_amount;
        Update_Power_Text();
    }

    public placing_status_enum Use_Power(int power_use_amount)
    {
        if (power_use_amount <= power && buttonCtrl.selected_gameobj)
        {
            power -= power_use_amount;
            Update_Power_Text();
            return placing_status_enum.S;
        }
        return placing_status_enum.F;
    }

    void Update_Power_Text()
    {
        power_text.text = power.ToString();
    }
}
