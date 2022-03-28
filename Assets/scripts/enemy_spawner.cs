using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy_spawner : MonoBehaviour
{
    public GameObject[] enemy_array;

    public int how_many_lanes;

    public int time_before_spawn;

    public Transform spawningPos;

    int floatTime;
    int min;
    int sec;
    public Text EnemyArrivesText;

    void Start()
    {
        floatTime = time_before_spawn;
        InvokeRepeating("Reduce_floatTime", 0.01f, 1);
    }
    void Reduce_floatTime()
    {
        floatTime -= 1;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > time_before_spawn)
        {
            foreach (GameObject theEnemy in enemy_array)
            {
                if (isTimeToSpawn(theEnemy))
                {
                    spawn(theEnemy);
                }
            }
        }

        if(EnemyArrivesText && floatTime >= 0)
        {
            min = floatTime / 60;
            sec = floatTime % 60;
            EnemyArrivesText.GetComponent<Text>().text = "Enemy Arrives In " + min.ToString() + ":" + sec.ToString();
        }
        else if(EnemyArrivesText && floatTime <= 0)
        {
            EnemyArrivesText.gameObject.active = false;
        }
    }

    void spawn(GameObject passed_enemy)
    {
        GetComponent<Animator>().SetTrigger("openDoor");
        Instantiate(passed_enemy, spawningPos.position, passed_enemy.transform.rotation);
    }

    bool isTimeToSpawn(GameObject passed_enemy)
    {
        float seen_every_second = passed_enemy.GetComponent<attack_mode>().seenEverySecond;

        float seen_per_second = 1 / seen_every_second;  // to reduce the value below 1;

        float threshold = seen_per_second * Time.deltaTime / how_many_lanes;

        return (Random.value < threshold);
    }
}
