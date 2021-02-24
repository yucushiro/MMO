using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;
public class login : MonoBehaviour
{
    public Text userText;
    public InputField userPass;
    public Text responseField;
    public string usuario;
    public string password;
    public string errorMessage;
    string rootURL = "http://ninjat-rexfactory.com/PKMH/";
    public string datosString;
    public string[] datos;
    public GameObject Datos_player;
    public bool autoriza_login;
    public float timer;
    
   
    

    private void Start()
    {
        autoriza_login = false;
        timer = 0;
    }

    public void Corrutinas()
    {
        StartCoroutine(Login());
    }
    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        usuario = userText.text;
        password = userPass.text;
        form.AddField("user", usuario);
        form.AddField("password", password);
        form.AddField("myform_hash", "hashcode");

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login_unity.php", form))
        {
            yield return www.SendWebRequest();
            
            if (www.isNetworkError)
            {
                errorMessage = www.error;
                responseField.text = errorMessage;
                
            }
            else
            {
                
                datosString = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
                datos = datosString.Split(' ');
                responseField.text = "Esperando Cineccion";
                Datos_player.GetComponent<Datos_player>().datos_player = datos;
                autoriza_login = true;
            }
        }





    }
    private void Update()
    {
        if (autoriza_login == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 10f)
        {
            SceneManager.LoadScene("Scene1",LoadSceneMode.Single);
        }
    }







}
