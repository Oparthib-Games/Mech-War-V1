using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocker_launcher_script : MonoBehaviour
{
    public GameObject missile;
    public Transform[] missile_pos;
    public Transform[] missile_waypoints;

    public Transform enemyCheckPos;
    public float range;
    public LayerMask mask;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyCheck();
    }

    void Rocket_Fire(int i)
    {
        Quaternion missile_rot = Quaternion.Euler(0,90,0);
        GameObject missile_GO = (GameObject)Instantiate(missile, missile_pos[i].position, missile_rot);
        missile_GO.GetComponent<missile_script>().myOrigin = gameObject.transform;
    }

    void EnemyCheck()
    {
        Ray ray_origin = new Ray(enemyCheckPos.position, enemyCheckPos.right);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray_origin, out hitInfo, range, mask)) anim.SetBool("isAttacking", true);
        else anim.SetBool("isAttacking", false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(enemyCheckPos.position, enemyCheckPos.position + enemyCheckPos.right * range);
    }
}
