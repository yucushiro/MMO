using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_Script : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Slider>().maxValue = Player.GetComponent<Stats>().Max_Stamina;
        this.gameObject.GetComponent<Slider>().value = Player.GetComponent<Stats>().Stamina;
    }
}
