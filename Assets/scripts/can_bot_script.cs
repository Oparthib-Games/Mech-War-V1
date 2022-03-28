using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class can_bot_script : MonoBehaviour
{
    public Transform enemyCheckRayPos;
    Animator anim;
    AudioSource audioSORS;
    public float range;
    public LayerMask enemyLayer;

    public float damageAmount;
    public GameObject Kicking_Obj;

    public GameObject kickingParticle;
    public Transform kickingParticlePos;

    public GameObject shield;
    public GameObject lathi;

    void Start()
    {
        anim = GetComponent<Animator>();
        audioSORS = GetComponent<AudioSource>();
    }

    void Update()
    {
        EnemyCheck();

        if (shield != null && Kicking_Obj == null) anim.SetBool("hasShield", true);
        else anim.SetBool("hasShield", false);

        if (lathi != null && Kicking_Obj == null) anim.SetBool("hasLathi", true);
        else anim.SetBool("hasLathi", false);
    }

    void EnemyCheck()
    {
        Ray rayOrigin = new Ray(enemyCheckRayPos.position, enemyCheckRayPos.right * -1 * range);
        RaycastHit hitInfo;

        if (!lathi)
        {
            if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) { anim.SetBool("isAttacking", true); Kicking_Obj = hitInfo.transform.gameObject; }
            else anim.SetBool("isAttacking", false);
        }
        else
        {
            if (Physics.Raycast(rayOrigin, out hitInfo, range, enemyLayer)) { anim.SetBool("pitao", true); Kicking_Obj = hitInfo.transform.gameObject; }
            else anim.SetBool("pitao", false);
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawLine(enemyCheckRayPos.position, enemyCheckRayPos.position + Vector3.left * range);
    }

    void Kick_Damage()
    {
        audioSORS.Play();

        Kicking_Obj.GetComponent<health_script>().dealDamage(damageAmount);

        Instantiate(kickingParticle, kickingParticlePos.position, Quaternion.identity);
    }
}
