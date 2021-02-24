using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Controler_Script : MonoBehaviour
{
    public Transform Player;
    public GameObject PlayerGO;
    public float MouseX;
    public float MouseY;
    public float SpeedMove;
    public float camera_Distance;
    public Vector3 PlayerLocation;
    
  

    //public GameObject Personage;

    // Start is called before the first frame update
    void Start()
    {
        SpeedMove = 10f;
        camera_Distance = 100f;
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocation = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        MouseX = Input.GetAxis("Mouse X") * SpeedMove;
        MouseY = Input.GetAxis("Mouse Y") * SpeedMove;
        if (Input.GetMouseButton(0))
        {

            //Debug.Log("Pressed left click.");

            
            transform.RotateAround(PlayerLocation, Vector3.up, 100f * MouseX * Time.deltaTime);
            transform.LookAt(PlayerLocation);

        }

        if (Input.GetMouseButton(1))
        {
            PlayerGO.transform.Rotate(Vector3.up,(MouseX*20f*Time.deltaTime));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //Zoom Dentro//
            transform.localPosition = transform.localPosition + transform.forward * Time.deltaTime*100f;
            transform.LookAt(PlayerLocation);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //Zoom Fuera//
            transform.localPosition = transform.localPosition - transform.forward * Time.deltaTime*100f;
            transform.LookAt(PlayerLocation);

        }
    }

}
