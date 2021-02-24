using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Ataque : MonoBehaviour
{
    public GameObject Player;
    public Vector3 KnockbackDirection;
    public GameObject Unarmed_Box_Colider_Left;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag=="NPC" && Player.GetComponent<CharacterControlerScript>().State=="Atack")
        {
          
            KnockbackDirection = other.gameObject.GetComponent<Transform>().position - Player.transform.position;
            if (Player.GetComponent<CharacterControlerScript>().State == "Atack")
            {
                other.GetComponent<Monster_Statement_Machine>().target = Player.gameObject;
                other.GetComponent<Monster_Statement_Machine>().DanoEntrante(Player.GetComponent<Stats>().Ataque);
                other.GetComponent<Rigidbody>().AddForce(KnockbackDirection * 1f, ForceMode.Impulse);
                //other.GetComponent<Rigidbody>().AddForce(transform.up * 1f, ForceMode.Impulse);
                other.GetComponent<Monster_Statement_Machine>().Estado = "knokedback";

            }

            this.gameObject.GetComponent<BoxCollider>().enabled = false;





        }
        
    }


}
