using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using UnityEngine.XR;

public class Monster_Statement_Machine : MonoBehaviour
{
    public Animator animator;
    public string Estado;
    public float timer;
    public Rigidbody RB;
    int Valor_Random_rotacion;
    int Valor_Random_Ciclos;
    public GameObject target;
    private GameObject bala_temporal;
    private Rigidbody bala_temporal_RB;
    public GameObject Proyectil_Prefab;
    public GameObject Emisor;
    public float proyectiles;
    private bool shoting;
    private Vector3 direccion_objetivo;
    public GameObject father;
    public float Dano_Recibido;
    public float Distancia_Minima;
    private bool Stamina_Refilled;
    public ParticleSystem Dashes;
    public ParticleSystem EmisorSplashes;
    public GameObject Squirtle;
    public Material EyesAngry;
    public Material EyesIdle;
    public Material EyesDead;
    public Material EyesKnokedback;
    public Material []Eyes;
    public bool Golpe_Fuerte;
    public bool Reververancebool;
    public bool contacto;

    


    // Start is called before the first frame update
    void Start()
    {
        Estado = "Idle";
        timer = 0.1f;
        RB = this.gameObject.GetComponent<Rigidbody>();
        Valor_Random_Ciclos = Random.Range(5, 10);
        Stamina_Refilled = true;
        Golpe_Fuerte = false;
        Reververancebool = false;
        contacto = false;

       

    }

    // Update is called once per frame
    void Update()
    {

        if (Estado == "Idle")
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, Valor_Random_rotacion, 0), 100 * Time.deltaTime);
            Eyes = Squirtle.GetComponent<SkinnedMeshRenderer>().materials;
            Eyes[1] = EyesIdle;
            Squirtle.GetComponent<SkinnedMeshRenderer>().materials = Eyes;

        }

        if (Estado == "Idle" && timer >= Valor_Random_Ciclos)
        {
            Patrulla();
            timer = 0;

        }


        ///Estado Patrulla/
        if (Estado == "Patrulla")
        {
            RB.transform.position += transform.forward / 2 * Time.deltaTime;
            if (timer >= Valor_Random_Ciclos)
            {
                timer = 0;
                Idle();




            }
        }

        if (Estado == "Atacando" || Estado == "Agresivo")
        {
            Eyes = Squirtle.GetComponent<SkinnedMeshRenderer>().materials;
            Eyes[1] = EyesAngry;
            Squirtle.GetComponent<SkinnedMeshRenderer>().materials = Eyes;
            
        }
        //////////////////////////////
        ////////Estado Atacando///////
        ///////////////////////////////


        if (Estado == "Atacando" && timer >= 2 && timer <= 20 && Distancia_Minima > 2f && Distancia_Minima < 5f)
        {
            RB.transform.position += transform.forward * 5 * Time.deltaTime;
            direccion_objetivo = target.transform.position - transform.position;
            direccion_objetivo.y = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direccion_objetivo), 100 * Time.deltaTime);
            
        }

        if (Estado == "Atacando" && Distancia_Minima<2f)
        {
            
        }



        ////////////////////////////
        ///////Estado Agresivo//////
        /////////////////////////////

        if (Estado == "Agresivo")
        {
            Dashes.Stop();
            Distancia_Minima = Vector3.Distance(target.transform.position, transform.position);
            if (gameObject.GetComponent<Monster_Status>().Stamina >= 0 && Stamina_Refilled == true)
            {
                
                ///////////////////////////////
                ////////////SPIN Atack/////////
                ///////////////////////////////
                if (timer >= 5 && timer <= 20 && Distancia_Minima <= 3f && Distancia_Minima > 2f)
                {
                    animator.Play("atack4");
                    Dashes.Play();
                    Estado = "Atacando";
                    Golpe_Fuerte = true;



                }




                ///////////////////////////////
                //////////// WatherBall///////
                ///////////////////////////////

                if (timer >= 5 && timer <= 20 && Distancia_Minima > 3f)
                {
                    Disparar();


                    if (shoting == true)
                    {
                        Debug.Log("Disparando");
                        bala_temporal_RB = bala_temporal.GetComponent<Rigidbody>();
                        bala_temporal_RB.AddForce(transform.forward * 10f, ForceMode.Impulse);
                        bala_temporal_RB.AddForce(transform.up * 3f, ForceMode.Impulse);
                        shoting = false;
                        Golpe_Fuerte = false;
                        timer = 0;
                        gameObject.GetComponent<Monster_Status>().Stamina -= 30;
                    }
                }

                //////////////////////////////////
                ////////   Reberberance  /////////
                //////////////////////////////////
                if (timer > 1 && contacto == true )
                {

                    Reververance();
                    Golpe_Fuerte = true;




                }

                if (timer <= 5 && timer >= 0 && contacto == false)
                {
                    animator.Play("walk2");
                    RB.transform.position += transform.forward / 1.2f * Time.deltaTime;
                    direccion_objetivo = target.transform.position - transform.position;
                    direccion_objetivo.y = 0;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direccion_objetivo), 1000 * Time.deltaTime); //Quaternion.LookRotation(direccion_objetivo);
                }





            }

            if (gameObject.GetComponent<Monster_Status>().Stamina <= 0 && Stamina_Refilled == true)
            {
                gameObject.GetComponent<Monster_Status>().Stamina = 0;
                Stamina_Refilled = false;
                timer = 0;

            }
            if (Stamina_Refilled == false)
            {
                if (timer == 0)
                {
                    animator.Play("exaust");
                }
                End();
            }








            // GetComponent<Monster_Status>().Disparar();
        }




        ///Timer Constante///

        timer += Time.deltaTime;
        /////////////////////
        ////Estatus DEAD////
        /////////////////////
        if (gameObject.GetComponent<Monster_Status>().hp <= 0)
        {
            Estado = "Dead";
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            animator.Play("dead");
            StartCoroutine("Putrefaccion");
            Eyes = Squirtle.GetComponent<SkinnedMeshRenderer>().materials;
            Eyes[1] = EyesDead;
            Squirtle.GetComponent<SkinnedMeshRenderer>().materials = Eyes;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;

        }

        /////////////////////////////////
        ////////Status Knokedback///////
        /////////////////////////////////

        if (Estado=="knokedback")
        {
            animator.Play("Squirtle_Hited");
            Eyes = Squirtle.GetComponent<SkinnedMeshRenderer>().materials;
            Eyes[1] = EyesKnokedback;
            Squirtle.GetComponent<SkinnedMeshRenderer>().materials = Eyes;

        }

    }
    public void Patrulla()
    {
        animator.Play("walk1");
        Estado = "Patrulla";
        
    }

    public void Idle()
    {
        animator.Play("idle");
        Estado = "Idle";
        Valor_Random_rotacion = Random.Range(30, 360);
        Valor_Random_Ciclos = Random.Range(5, 10);


    }

    public void Agresive()
    {
        animator.Play("walk2");
        Estado = "Agresivo";
        timer = 0;


    }

    public void Disparar()
    {
        
        
        animator.Play("atack1");



    }

    public void Shot()
    {
        bala_temporal = Instantiate(Proyectil_Prefab, Emisor.transform.position, Emisor.transform.rotation);
        proyectiles -= 1;
        shoting = true;
        

        
    }
    public void End()
    {
        
        shoting = false;
        StartCoroutine("RecuperaEstamina");
        Estado = "Agresivo";
        timer = 0;

    }
    public void EndExaust()
    {
        //animator.Play("walk2");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Respawn")
        {
            father = other.gameObject;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contacto = true;
        }
        else
        {
            contacto = false;
        }

    }


    public void DanoEntrante(float ataqueEnemigo)
    {
        
        Dano_Recibido = ataqueEnemigo - gameObject.GetComponent<Monster_Status>().Denfensa;
        if (Dano_Recibido <= 0)
        { Dano_Recibido = 0; }
        if (Dano_Recibido > 0)
        {
            
            
            Debug.Log("Recibio"+ Dano_Recibido);
            target.gameObject.GetComponent<Stats>().Alertas_Texto(Dano_Recibido, this.gameObject.name);
            //RB.AddForce(KnockbackDirection * 100f, ForceMode.Impulse);
            //RB.AddForce(transform.up * 10f, ForceMode.Impulse);
            gameObject.GetComponent<Monster_Status>().hp -= Dano_Recibido;
           
        }





    }


    public void Reververance()
    {
        animator.Play("atack2");
        Reververancebool = true;
        
    }

    IEnumerator RecuperaEstamina()
    {
        //animator.Play("exaust");
        if (gameObject.GetComponent<Monster_Status>().Stamina <= 100)
        {
            gameObject.GetComponent<Monster_Status>().Stamina += Time.deltaTime * 10;
            
        }

        if (gameObject.GetComponent<Monster_Status>().Stamina >= 100)
        {
            Stamina_Refilled = true;

            yield return null;
        }
    }

    IEnumerator Putrefaccion()
    {
       
        yield return new WaitForSecondsRealtime(15);
        Destroy(this.gameObject);


    }












}
