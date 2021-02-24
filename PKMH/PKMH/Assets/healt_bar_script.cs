using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healt_bar_script : MonoBehaviour
{
    public GameObject healtbart;
    public Gradient gradiente;
    public GameObject player;
    public Image fill;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healtbart.GetComponent<Slider>().maxValue = player.GetComponent<Stats>().Max_HP;
        healtbart.GetComponent<Slider>().value = player.GetComponent<Stats>().HP;
        fill.color = gradiente.Evaluate(slider.normalizedValue);
    }
}
