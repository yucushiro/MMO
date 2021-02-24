using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float HP;
    public float Ataque;
    public float Defensa;
    public float Stamina;
    public Text textoHP;
    public Text Alertas;
    public Rigidbody RB;
    public Animator animator;
    public GameObject trigger_Right;
    public GameObject trigger_Left;
    public Vector3 KnockbackDirection;
    public bool knockbacked;
    public bool canTakeDamage;
    public bool Golpe_Fuerte;
    public float Max_HP;
    public float Max_Stamina;


    // Start is called before the first frame update
    void Start()
    {
        knockbacked = false;
        canTakeDamage = true;
        Golpe_Fuerte = false;

    }

    // Update is called once per frame
    void Update()
    {
        //textoHP.text = "HP + " + HP;
        if (knockbacked == true)
        {


            if (Golpe_Fuerte == false)
            {
                animator.Play("Dañado");
                RB.AddForce(KnockbackDirection.normalized / 2.8f, ForceMode.Impulse);
                RB.AddForce(transform.up.normalized / 2.9f, ForceMode.Impulse);

            }
            if (Golpe_Fuerte == true)
            {
                animator.Play("Hard_Knockback");
                RB.AddForce(KnockbackDirection.normalized / 2.8f, ForceMode.Impulse);
                RB.AddForce(transform.up.normalized / 2.7f, ForceMode.Impulse);

            }
        }

        /////Stamina Regen//////
        if (Stamina < 0)
        { Stamina = 0; }

        if (Stamina < Max_Stamina)
        {
            Stamina += 1 * Time.deltaTime;
        }




    }

    public void DanoRealizado()
    { }
    public void DanoRecibido(float dano)
    {
        HP -= dano;
        Debug.Log(dano);
    }



    void OnCollisionEnter(Collision collision)
    {

        ///////////////////Si recibe proyectiles/////////////////////
        if (collision.gameObject.tag == "Proyectil")
        {
            DanoRecibido(collision.gameObject.GetComponent<proyectil>().Ataque);
            animator.Play("Dañado");
            this.gameObject.GetComponent<CharacterControlerScript>().State = "knokedback";
            knockbacked = true;
            StartCoroutine("Espera_Para_Daño");

        }
        if (collision.transform.position.y < this.gameObject.transform.position.y && !collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Colison_Superior");
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "NPC" && collision.gameObject.GetComponent<Monster_Statement_Machine>().Estado == "Atacando")
        {
            KnockbackDirection = transform.position - collision.gameObject.GetComponent<Transform>().position;

            if (canTakeDamage == true)
            {
                DanoRecibido(collision.gameObject.GetComponent<Monster_Status>().Ataque);
                knockbacked = true;
                this.gameObject.GetComponent<CharacterControlerScript>().State = "knokedback";
                canTakeDamage = false;
                Golpe_Fuerte = collision.gameObject.GetComponent<Monster_Statement_Machine>().Golpe_Fuerte;
                if (Golpe_Fuerte == false) { StartCoroutine("Espera_Para_Daño"); }
                if (Golpe_Fuerte == true) { StartCoroutine("Espera_Para_Daño_Fuerte"); }

            }


        }



    }

    IEnumerator Espera_Para_Daño()
    {

        yield return new WaitForSecondsRealtime(0.08f);
        canTakeDamage = true;
        knockbacked = false;
        Debug.Log("Corrutina_Entra");

    }
    IEnumerator Espera_Para_Daño_Fuerte()
    {

        yield return new WaitForSecondsRealtime(0.5f);
        canTakeDamage = true;
        knockbacked = false;
        Debug.Log("Corrutina_Entra");

    }

    public void Alertas_Texto(float AlertasdeTexto, string ObjetoenCuestion)
    {
        Alertas.text= "Has Relizado "+AlertasdeTexto+ " a " + ObjetoenCuestion;

    }

   


}
