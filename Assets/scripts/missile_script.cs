using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile_script : MonoBehaviour
{
    public Transform[] missile_tracks;

    public Transform myOrigin;

    public GameObject explosion;

    public AudioClip clip;

    public float speed;

    public float DamageAmount;

    int i = 0;

    public int where_to_find_track;

    Transform target;

    [Tooltip("only for red Rocket.")]
    public Transform all_red_rocket_track;

    void Start()
    {
        if (where_to_find_track == 1)
        {
            missile_tracks = myOrigin.GetComponent<Rocker_launcher_script>().missile_waypoints;
        }
        else if(where_to_find_track == 2)
        {
            missile_tracks = myOrigin.GetComponent<titan_script>().missile_waypoints;
        }
        else if (where_to_find_track == 3)
        {
            missile_tracks = all_red_rocket_track.GetComponentsInChildren<Transform>();
        }

        target = missile_tracks[0].transform;

        Destroy(gameObject, 5);
    }

    void Update()
    {
        if(myOrigin == null && where_to_find_track != 3)
        {
            Destroy(gameObject);
        }

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == missile_tracks[i].transform.position)
        {
            i++;
            target = missile_tracks[i].transform;
        }
    }

    void OnTriggerEnter(Collider trig)
    {
        GameObject spaw_as_GO = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
        if(where_to_find_track == 1)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.165f);
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 0.33f);
        }
        trig.GetComponent<health_script>().dealDamage(DamageAmount);
        Destroy(gameObject);
    }
}
