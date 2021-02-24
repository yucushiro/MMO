using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_script : MonoBehaviour
{
    public Animator animator;
   


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        
        
    }
    


    

    // Update is called once per frame
    void Update()
    {
        

     
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Saltando");
            //animator.SetBool("jump", true);
            //animator.SetBool("walk", false);

        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //animator.SetBool("Fall", true);
            animator.SetBool("jump", true);
            animator.SetBool("walk", false);
        }





    }


}
