using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_script : MonoBehaviour
{
    [Header("bullet")]
    public GameObject bullet;
    public Transform bullet_pos;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPos;

    Animator anim;
    AudioSource audioSORS;

    Ray rayOrigin;
    RaycastHit hitInfo;

    public Transform enemyCheckRayPos;
    public float range;
    public LayerMask enemyLayer;

    void Start()
    {
        rayOrigin = new Ray(enemyCheckRayPos.position, Vector3.right);

        anim = GetComponent<Animator>();
        audioSORS = GetComponent<AudioSource>();

        InvokeRepeating("EnemyCheck", 0, 0.5f);
    }


    void EnemyCheck()
    {
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) anim.SetBool("isAttacking", true);
        else anim.SetBool("isAttacking", false);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.right * range);
    }

    public void Shoot()
    {
        float random_range = Random.Range(-0.5f, 0.5f);
        Vector3 instatiate_pos  =new Vector3 (bullet_pos.position.x + random_range, bullet_pos.position.y + random_range, bullet_pos.position.z);
        audioSORS.Play();
        Instantiate(bullet, instatiate_pos, bullet.transform.rotation);
        Instantiate(muzzleFlash, muzzleFlashPos.position, Quaternion.identity);

    }
}
