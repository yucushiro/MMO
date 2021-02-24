using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public float timer;
    public GameObject []pokemones;
    private int valorRandom;
    public GameObject Emisor;
    public float EmisionesMaximas;
    public Vector3 Random_Respawn;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            Respawn();
        }
    }
    private void Respawn()
    {
        if (EmisionesMaximas > 0)
        {

            Random_Respawn.x = Random.Range((-transform.localScale.x / 2), (transform.localScale.x / 2));
            Random_Respawn.y = transform.localScale.y + 5;
            Random_Respawn.z = Random.Range((-transform.localScale.z / 2), (transform.localScale.z / 2));
            valorRandom = Mathf.FloorToInt((Random.Range(0, 3)));

            Instantiate(pokemones[valorRandom], Emisor.transform.position + new Vector3(Random_Respawn.x, 5, Random_Respawn.z), Emisor.transform.rotation);
            timer = 0;
            EmisionesMaximas -= 1;
        }
    }
}
