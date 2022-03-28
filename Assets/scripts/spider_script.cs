using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider_script : MonoBehaviour
{

    public GameObject suicide_explosion;

    public float damage;

    public string whoAmI;

    public GameObject[] spider;

    public Transform spider_spawn_pos;

    public int how_many_spider_to_spawn;

    public AudioClip clip;
    
    void Start()
    {
        //if (whoAmI == "spider_spawner")
        //{
        //    //InvokeRepeating("spawn_spider", 2, 1);
        //    //Invoke("spawn_spider", 5);
        //}
    }

    void Update()
    {

    }
        
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Good_Guys" && whoAmI == "spider")
        {
            Instantiate(suicide_explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 3f);
            col.gameObject.GetComponent<health_script>().dealDamage(damage);
            Destroy(gameObject);
        }
    }

    public void spawn_spider()
    {
        int Xx = Random.Range(0, 5);

        Instantiate(spider[Xx], spider_spawn_pos.position, spider[Xx].transform.rotation);

        //for (int i = 0; i < how_many_spider_to_spawn; i++)
        //{
        //    Instantiate(spider[Xx], spider_spawn_pos[x].position, spider[Xx].transform.rotation);
        //}
    }

    public void Destroy_Spider_Spawner()
    {
        GetComponent<health_script>().dealDamage(2000);
    }

}
