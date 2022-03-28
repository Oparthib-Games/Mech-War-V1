using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placing_script : MonoBehaviour
{
    public Color responsive_color;
    Color original_color;
    public Material base_mat_access;

    public bool isEmpty = true;

    power_display_script power_display_script_access;

    public GameObject object_placed_on_me;

    public GameObject PlacingParticle;

    public AudioClip clip;

    //public LayerMask mask;

    void Start()
    {
        base_mat_access = GetComponent<Renderer>().material;
        original_color = base_mat_access.color;

        power_display_script_access = FindObjectOfType<power_display_script>();
    }

    void Update()
    {
        //check_if_something_on_me();

        if (object_placed_on_me == null)
        {
            isEmpty = true;
        }
        else { isEmpty = false; }
    }

    void OnMouseUp()
    {
        if(Panning.isPanning == false)
        {
            int pass_cost = buttonCtrl.placing_cost;

            if (isEmpty && power_display_script_access.Use_Power(pass_cost) == power_display_script.placing_status_enum.S)
            {
                GameObject spawn_as_GO = (Instantiate(buttonCtrl.selected_gameobj, transform.position, Quaternion.identity));
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.3f);
                GameObject spawn_as_GO_particle = (Instantiate(PlacingParticle, transform.position + new Vector3(0, .4f, 0), Quaternion.identity));
                object_placed_on_me = spawn_as_GO;
                isEmpty = false;
            }
            else
            {
                print("not empty");
            }
        }
    }

    //void check_if_something_on_me()
    //{
    //    Ray rayOrigin = new Ray(transform.position + new Vector3(0,1,0), transform.position + transform.up * -1 * 100);
    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(rayOrigin, out hitInfo, 100, mask)) {isEmpty = false;}
    //    else {isEmpty = true;}
    //}

    void OnMouseOver()
    {
        base_mat_access.color = responsive_color;
    }
    void OnMouseExit()
    {
        base_mat_access.color = original_color;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 10);
    //}
}
