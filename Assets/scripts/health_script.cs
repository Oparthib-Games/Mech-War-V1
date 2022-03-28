using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_script : MonoBehaviour
{
    public float health;

    public GameObject destroy_particle;

    public bool isBuilding;

    public AudioClip clip;

    void Update()
    {
        if (health <= 0 && !isBuilding)
        {
            Instantiate(destroy_particle, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(gameObject);
        }
        else if (health <= 0 && isBuilding)
        {
            FindObjectOfType<lose_manager>().GetComponent<lose_manager>().Lose_life();
            Instantiate(destroy_particle, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.8f);
            if(this.gameObject.tag == "Enemy")
            {
                Enemy_Count.enemy_destroyed++;
            }
            Destroy(gameObject);
        }
    }

    public void dealDamage(float damage_amount)
    {
        health -= damage_amount;
    }
}
