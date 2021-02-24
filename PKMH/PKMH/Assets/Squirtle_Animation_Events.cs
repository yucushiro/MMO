using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirtle_Animation_Events : MonoBehaviour
{

    public GameObject Squirtle;
    public ParticleSystem Dashes;
    // Start is called before the first frame update
    public void Shot()
    {
        Squirtle.GetComponent<Monster_Statement_Machine>().Shot();
        
    }

    public void End()
    {
        Squirtle.GetComponent<Monster_Statement_Machine>().End();
        Dashes.Stop();
    }
    public void EndExaust()
    {
        Squirtle.GetComponent<Monster_Statement_Machine>().EndExaust();
    }
    public void StartRoll()
    {
        Squirtle.GetComponent<Monster_Status>().Stamina -= 30;
    }
    public void StopRoll()
    { }

    public void Dead()
    {
        Debug.Log(this.gameObject.name + " Esta Muertooo");
        Dashes.Stop();
    }

    public void End_Hited()
    {
        
        Squirtle.GetComponent<Monster_Statement_Machine>().timer = 0;
        Squirtle.GetComponent<Monster_Statement_Machine>().Estado = "Agresivo";

    }

    public void StartReververance()
    {
        
        
    }

    public void EndReververance()
    {
        Squirtle.GetComponent<Monster_Statement_Machine>().EmisorSplashes.Play();
        Squirtle.GetComponent<Monster_Statement_Machine>().Estado = "Atacando";
        Squirtle.GetComponent<Monster_Status>().Stamina -= 30;
    }



}
