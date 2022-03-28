using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lose_manager : MonoBehaviour
{

    public int life_count;

    public GameObject[] life;

    void Start()
    {
        life = GameObject.FindGameObjectsWithTag("life");
        life_count = life.Length;
    }
    
    void Update()
    {
        
    }

    public void Lose_life()
    {
        life_count--;
        Losing_Check();
        Destroy(life[life_count]);
    }

    void Losing_Check()
    {
        if (life_count <= 0)
        {
            FindObjectOfType<Menu_script>().GetComponent<Menu_script>().Losing();
        }
    }
}
