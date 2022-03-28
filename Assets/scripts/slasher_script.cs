using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slasher_script : MonoBehaviour
{
    public Transform enemyCheckRayPos;
    Animator anim;
    AudioSource audioSORS;
    public float range;
    public float range2;
    public LayerMask enemyLayer;
    public LayerMask enemyLayer2;

    public float damageAmount;

    GameObject slashing_obj;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSORS = GetComponent<AudioSource>();

        InvokeRepeating("JumpCheck", 0.001f, 1.5f);
    }

    void Update()
    {
        // JumpCheck();
        EnemyCheck();
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) { anim.SetBool("isAttacking", true); slashing_obj = hitInfo.transform.gameObject; }
        else anim.SetBool("isAttacking", false);
    }

    void JumpCheck()
    {
        Ray rayOrigin2 = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range2);
        RaycastHit hitInfo2;
        if (Physics.Raycast(rayOrigin2, out hitInfo2, range2, enemyLayer2)) 
        {
                anim.SetTrigger("jump");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.left * range);
    }

    void Slash_Damage()
    {
        audioSORS.Play();

        slashing_obj.GetComponent<health_script>().dealDamage(damageAmount);
    }
}
