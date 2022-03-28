using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titan_script : MonoBehaviour
{
    public Transform enemyCheckRayPos;
    Animator anim;
    public float range;
    public LayerMask enemyLayer;

    public GameObject rocket;
    public GameObject rocket_handle;
    public Transform[] rocket_pos;
    public Transform[] missile_waypoints;

    public GameObject target;

    void Start()
    {
        anim = GetComponent<Animator>();

        //Transform[] rocket_pos = new Transform[transform.childCount];
        //for (int x = 0; x < transform.childCount; x++)
        //{
        //    rocket_pos[x] = transform.GetChild(x);
        //}

        rocket_pos = rocket_handle.GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        EnemyCheck();

        if(target)
        {
            missile_waypoints[1] = target.transform;
        }
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) {anim.SetBool("isAttacking", true); target = hitInfo.transform.gameObject;}
        else anim.SetBool("isAttacking", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.left * range);
    }
    void Rocket_Fire()
    {
        int i = Random.Range(0, 6);
        Quaternion rocket_rot = Quaternion.Euler(0, -90, 0);
        GameObject rocket_GO = (GameObject)Instantiate(rocket, rocket_pos[i].position, rocket_rot);
        rocket_GO.GetComponent<missile_script>().myOrigin = gameObject.transform;
    }

}
