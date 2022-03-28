using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOMBxMINES_script : MonoBehaviour
{
    public bool isMine;

    public GameObject bombExplosion;
    public GameObject mineExplosion;

    public Vector3 BombDirection;

    public LayerMask mask;
    public float bomb_area;

    Rigidbody RB;

    public AudioClip clip;

    public float damage;


    void Start()
    {
        RB = GetComponent<Rigidbody>();

        if (!isMine) { RB.AddForce(BombDirection * Time.deltaTime, ForceMode.Impulse); }
    }

    void Update()
    {
        if(isMine == false) //means is BOMB
        {
            Collider[] hit_col = Physics.OverlapSphere(transform.position, bomb_area, mask);

            int i = 0;

            while (i < hit_col.Length)
            {
                Instantiate(bombExplosion, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                hit_col[i].gameObject.GetComponent<Animator>().SetTrigger("isDisable");
                hit_col[i].gameObject.GetComponent<health_script>().dealDamage(damage);
                i++;
            }
        }
    }

    void OnTriggerEnter(Collider trig)
    {
        if (!isMine)
        {
            Instantiate(bombExplosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }

        if(trig.gameObject.GetComponent<Animator>())
        {
            trig.gameObject.GetComponent<Animator>().SetTrigger("isDisable");
        }

        if(isMine)
        {
            if(trig.tag == "Enemy")
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                Instantiate(mineExplosion, transform.position, Quaternion.identity);
                trig.gameObject.GetComponent<health_script>().dealDamage(damage);
            }
        }


        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, bomb_area);
    }
}
