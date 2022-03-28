using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tesla_script : MonoBehaviour
{
    public int power_production;

    public GameObject thunder;
    public Vector3 thunder_pos;

    public GameObject power_sign;
    public Vector3 power_sign_pos;

    public GameObject click_impact;
    public AudioClip clip;

    public string whoAmI;

    void produce_power()
    {
        Instantiate(thunder, transform.position + thunder_pos, Quaternion.identity);
        Instantiate(power_sign, transform.position + power_sign_pos, power_sign.transform.rotation);
    }

    void OnMouseDown()
    {
        if(whoAmI == "power_sign")
        {
            FindObjectOfType<power_display_script>().Add_Power(power_production);
            Instantiate(click_impact, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.5f);
            Destroy(gameObject);
        }
    }

    void Power_Destroy()
    {
        Destroy(gameObject);
    }
}
