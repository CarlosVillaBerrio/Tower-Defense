using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElTiempo : MonoBehaviour
{
    public bool lanzarOla_2 = false;
    public bool lanzarOla_3 = false;
    float fTiempo = 330f;
    int tiempo;
    public Text textTime;

    void Start()
    {
        
    }

    void Update()
    {
        fTiempo -= Time.deltaTime;
        tiempo = (int)fTiempo;
        ActualizarTiempoCanvas();
    }

    void ActualizarTiempoCanvas()
    {
        textTime.GetComponent<Text>().text = tiempo.ToString();
        ActivadorDeOleadasPorTiempo();
    }

    void ActivadorDeOleadasPorTiempo()
    {
        if(tiempo <= 250)
        {
            lanzarOla_2 = true;
        }
        if (tiempo <= 120)
        {
            lanzarOla_3 = true;
        }
        if(tiempo <= 0)
        {
            SceneManager.LoadScene("FantasyWin");
        }
    
    }
}
