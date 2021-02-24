using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LandScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem Land_Dust;
    public Animator animator;
    public GameObject Player;
    public bool isHiting;
    public GameObject Unarmed_Colider_Right;
    public GameObject Unarmed_Colider_Left;
    public float timer_Combo;
    public bool timer_Combo_runing;
    public float Combo_state;

    private void Start()
    {
        Unarmed_Colider_Left.GetComponent<BoxCollider>().enabled = false;
        timer_Combo = 0;
        timer_Combo_runing = false;
        Combo_state = 0;
    }


    void Update()
    {
        if (timer_Combo_runing== true)
        {
            timer_Combo = timer_Combo + Time.deltaTime;
        }

        if (timer_Combo >= 3f || Combo_state>=3)
        {
            timer_Combo_runing = false;
            timer_Combo = 0;
            Combo_state = 0;
            Player.GetComponent<CharacterControlerScript>().State = "idle";
        }
        
    }

    public void LandEnd()
    {
        Land_Dust.Play();
        
    }

    ///Eventos de animaciones///
    public void End()
    {

        
        //animator.Play("Idle");
        Player.GetComponent<CharacterControlerScript>().State = "idle";
        
    }

    public void Hit()
    {
        if (Combo_state == 0 || Combo_state == 2)
        { Unarmed_Colider_Left.GetComponent<BoxCollider>().enabled = true; }
        if (Combo_state == 1)
        { Unarmed_Colider_Right.GetComponent<BoxCollider>().enabled = true; }
        timer_Combo_runing = true;
        Combo_state += 1;
        Player.GetComponent<Stats>().Stamina -= 10;

    }


    public void EndHIt()
    {
        Unarmed_Colider_Left.GetComponent<BoxCollider>().enabled = false;
        Unarmed_Colider_Right.GetComponent<BoxCollider>().enabled = false;
    }

    public void End_Damage()
    {
        //animator.Play("Idle");
    }

    public void End_Knockback()
    {
        Player.GetComponent<Stats>().knockbacked = false;
        Player.GetComponent<Stats>().Golpe_Fuerte = false;
        
    }

   



}
