using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterControlerScript : MonoBehaviour
{
    public float Actual_Angle;
    public float speed /*= 20.0f*/;
    // float rotSpeed = 150f;
    public Rigidbody RB;
    // float yRo;
    private GameObject Player;
    public float PlayerGravity = -9.8f;
    public float HSpeed;
    public bool isGrounded;
    public Animator animator;
    //public Text TimerText;
    public float Coldown;
    private float Coldown_Base;
    //Funciones de Animator
    public LandScript LS;
    public string State;
    float smoothTime = 0.1f;
    float turnsmootVelocity;
    public Transform cam;



    // Start is called before the first frame update
    void Start()
    {
        Player = this.gameObject;
        //yRo = 0f;
        HSpeed = 0f;
        speed = 5f;
        RB = GetComponent<Rigidbody>();
        isGrounded = false;
        Coldown_Base = Coldown;
        State = "idle";
        

    }


    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f && State == "idle")
        {
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            //Player.transform.rotation = Quaternion.Euler(0f,targetAngle,0f);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnsmootVelocity,smoothTime);
            Vector3 moveDirection = Quaternion.Euler(0f, angle, 0f)*Vector3.forward;
            Player.transform.position += moveDirection * Time.deltaTime * speed;
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation,Quaternion.LookRotation(moveDirection), Time.deltaTime*1000f);
            
        }



        //Timer//
        Coldown -= Time.deltaTime;
        //TimerText.text = "Coldown:" + Coldown.ToString();
        //Fin del Timer
        if (Input.GetKey(KeyCode.LeftShift))
        { speed = 10f; }
        else { speed = 5f; }

        if (direction.magnitude <= 0)
        {
            animator.SetBool("walk", false);
        }


        if (Input.GetKey(KeyCode.W) && State == "idle")
        {
            
            animator.SetFloat("DireccionFrontal", 1);
            animator.SetBool("walk", true);
        }
        if (Input.GetKey(KeyCode.S) && State == "idle")
        {
           
            animator.SetFloat("DireccionFrontal", 1);
            animator.SetBool("walk", true);
            //animator.Play("Walk");
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded && State != "Atack")
        {

            if (Coldown <= 0)
            {
                Salto();
                //RB.AddForce(new Vector3(0, 2f, 0), ForceMode.Impulse);
                Timer_Start();

            }
        }

        ///Animator///
        if (State == "idle")
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && isGrounded)
            {
                animator.SetFloat("DireccionFrontal", 1);
                animator.SetBool("walk", true);
                

            }


            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded)
            {
                animator.SetBool("walk", true);
                

            }

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetFloat("DireccionFrontal", 0);
            }
            if (Input.GetKey(KeyCode.Space) && !isGrounded)

            {
                Debug.Log("Saltando");
                animator.SetBool("jump", true);
                animator.SetBool("grounded", false);


            }
            else if (!Input.GetKey(KeyCode.Space) && isGrounded)
            {
                
                animator.SetBool("jump", false);
                animator.SetBool("grounded", true);

            }

            if (Input.GetKey(KeyCode.A)  && isGrounded)
            {
                animator.SetBool("walk", true);
            
                animator.SetFloat("DireccionFrontal", 1);

            }

            if (Input.GetKey(KeyCode.D)  && isGrounded)
            {
                animator.SetBool("walk", true);
               
                animator.SetFloat("DireccionFrontal", 1);

            }




            if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded == true && this.gameObject.GetComponent<Stats>().Stamina>20)
            {
                if (LS.GetComponent<LandScript>().Combo_state == 0)
                { animator.Play("1Atack"); }
                if (LS.GetComponent<LandScript>().Combo_state == 1)
                { animator.Play("2Atack"); }
                if (LS.GetComponent<LandScript>().Combo_state == 2)
                { animator.Play("3Atack"); }
                State = "Atack";

            }

        }



    }

 



    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("grounded", true);
            //Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider collision)
    {


        if (collision.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Out");
            isGrounded = false;
            animator.SetBool("grounded", false);
        }

    }

    void Timer_Start()
    {
        Coldown = Coldown_Base;
       

    }
    void Salto()
    {
        RB.AddForce(new Vector3(0, 10f, 0), ForceMode.Impulse);
    }
   


}
