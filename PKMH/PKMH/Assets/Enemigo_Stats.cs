using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Stats : MonoBehaviour
{
    // Start is called before the first frame update
    public float healt;
    public float dano = 10;
    public float def = 1;
    public Animator cubeAnim;
    private float coldown;
    public string mode;
    public SpriteRenderer healt_bar;
    public Rigidbody RB_Enemigo;
    private Vector3 KnockbackDirection;
    void Start()
    {
        coldown = 3f;
        mode = "idle";
        healt = 10;
    }


    

    // Update is called once per frame
    void Update()
    {
        coldown -= Time.deltaTime * 1f;
        if (coldown <= 0)
        { 
            Ciclo_de_Ataque();
        }

        if (healt <= 0f)
        { Destroy(gameObject); }
       
  
    }

    public void Ciclo_de_Ataque()
    {
        cubeAnim.Play("cubo_ataque");
        mode = "ataque";
        
    }

    public void End_Damage()
    {
        mode = "idle";
        coldown = 3f;
        cubeAnim.Play("Idle");
        
    }
  

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            KnockbackDirection = transform.position - collision.gameObject.GetComponent<Transform>().position;
            if (collision.gameObject.GetComponent<CharacterControlerScript>().State == "Atack")
            {
              DanoEntrante(collision.gameObject.GetComponent<Stats>().Ataque);
            }
        }
    }

    public void DanoEntrante(float ataqueEnemigo)
    {
         
        healt_bar.size -= new Vector2(ataqueEnemigo / 10f, 0);
        healt -= (ataqueEnemigo-def);
        RB_Enemigo.AddForce(KnockbackDirection.normalized * 100f, ForceMode.Impulse);
        RB_Enemigo.AddForce(transform.up * 50f, ForceMode.Impulse);
        




    }

    public void DanoSaliente(float DefensaEnemigo)
    {  }
}
