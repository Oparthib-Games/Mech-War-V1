using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    enum spawn_state_enum { SPAWNING, FIGHTING, TimeToSpawnWave };

    [System.Serializable]
    public class Wave_Info
    {
        public string name;
        public GameObject[] enemyAry;
        //public int enemy_count;
        public float rate;  //Time between per enemy in one wave.
    }

    public Wave_Info[] WaveAry; //Contains the Array of the Wave_Info Class

    public int X = 0;

    public float TimeBeforeEnemySpawn = 20f;
    public float timeBetweenWave = 5f;  //Time between one wave to another.
    public float waveCountDown;

    public float Search_Countdown = 7f;

    spawn_state_enum spawn_state = spawn_state_enum.TimeToSpawnWave;

    public Transform[] spawnPoint;

    int which_level_to_unlock;
    public Text EnemyArrivesText;

    void Start()
    {
        waveCountDown += TimeBeforeEnemySpawn;

        which_level_to_unlock = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void Update()
    {
        if(spawn_state == spawn_state_enum.FIGHTING)
        {
            if(All_Enemies_Dead())
            {
                spawn_state = spawn_state_enum.TimeToSpawnWave;
                X++;
            }
        }

        if(Time.timeSinceLevelLoad >= TimeBeforeEnemySpawn)
        {
            //EnemyArrivesText.gameObject.active = false;

            if (Time.timeSinceLevelLoad >= waveCountDown && spawn_state == spawn_state_enum.TimeToSpawnWave && X<WaveAry.Length) 
            {
                waveCountDown += timeBetweenWave;

                StartCoroutine(SpawnWave(WaveAry[X]));
            }
        }

        if(X >= WaveAry.Length)
        {
            if (PlayerPrefs.GetInt("level_reached") < SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("level_reached", which_level_to_unlock);
            }
            FindObjectOfType<Menu_script>().GetComponent<Menu_script>().Winning();
        }


        if(EnemyArrivesText && Time.timeSinceLevelLoad < TimeBeforeEnemySpawn)
        {
            EnemyArrivesText.GetComponent<Text>().text = "ENEMY ARRIVES IN: " + Mathf.RoundToInt(TimeBeforeEnemySpawn - Time.timeSinceLevelLoad);
        }
    }

    //______________________________________Spawn Every Enemy of A Single Wave________________________________________________

    IEnumerator SpawnWave(Wave_Info passed_wave)
    {
        Debug.Log("Spawning Wave: " + passed_wave.name);

        EnemyArrivesText.GetComponent<Text>().text = "WAVE: " + (X+1);
        EnemyArrivesText.GetComponent<Text>().fontSize = 25;

        spawn_state = spawn_state_enum.SPAWNING;

        for (int i = 0; i < passed_wave.enemyAry.Length; i++)   // spawns every enemy on a wave according to the wave rate.
        {
            int random_spawn_point = Random.Range(0, spawnPoint.Length);
            int random_enemy = Random.Range(0, passed_wave.enemyAry.Length);
            Vector3 spawnPointOffset = new Vector3(0f, -5.697f, 0f);

            spawnPoint[random_spawn_point].GetComponent<Animator>().SetTrigger("openDoor");

            Instantiate(passed_wave.enemyAry[random_enemy], spawnPoint[random_spawn_point].position + spawnPointOffset, Quaternion.identity);

            yield return new WaitForSeconds(passed_wave.rate);  // waits certain amount of time here.
        }

        spawn_state = spawn_state_enum.FIGHTING;    //After the spawning state there is the fighting state

        yield break;
    }

    //_________________________________________Check if All Enemies are Dead or Not____________________________________________

    bool All_Enemies_Dead()
    {
        Search_Countdown -= Time.deltaTime;

        if(Search_Countdown <= 0)
        {
            Search_Countdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return true;
            }
        }
        return false;
    }

}
