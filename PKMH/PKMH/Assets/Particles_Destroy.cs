using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_Destroy : MonoBehaviour
{
    public GameObject particulas_a_Destruir; 
    // Start is called before the first frame update
    void Start()
    {
        particulas_a_Destruir = this.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!particulas_a_Destruir.GetComponent<ParticleSystem>().IsAlive())
        {

            Destroy(gameObject);
        }
    }
}
