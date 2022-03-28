using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{
    Rigidbody RB;

    public GameObject bullet_shatter_particle;
    public Vector3 bulletDirection;
    public float damageAmount;

    public AudioClip clip;

    public string whoAmI;

    void Start()
    {
        RB = GetComponent<Rigidbody>();

        if (whoAmI == "fireBall")
        {
            RB.AddForce(bulletDirection * Time.deltaTime, ForceMode.Impulse);
        }
        
    }

    void Update()
    {
        if (whoAmI == "bullet")
        {
            transform.Translate(bulletDirection * Time.deltaTime);
        }
        else if (whoAmI == "canon")
        {
            transform.Translate(bulletDirection * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider trig)
    {
        if(whoAmI == "electricity")
        {
            trig.GetComponent<health_script>().dealDamage(damageAmount);
            trig.GetComponent<Animator>().SetTrigger("isStunned");
            return;
        }

        GameObject spawnAsGO = Instantiate(bullet_shatter_particle, transform.position, Quaternion.identity) as GameObject;
        if(whoAmI != "bullet") AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.25f);
        trig.gameObject.GetComponent<health_script>().dealDamage(damageAmount);
        Destroy(gameObject);
    }

}
