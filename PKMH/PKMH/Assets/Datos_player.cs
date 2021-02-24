using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datos_player : MonoBehaviour
{
    
    public string[] datos_player;
    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(this.gameObject);
    }


}
