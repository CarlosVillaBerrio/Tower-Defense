using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElTiempo : MonoBehaviour
{
    public bool lanzarOla_2 = false;
    public bool lanzarOla_3 = false;
    float fTiempo = 0;
    int tiempo;
    public Text textTime;

    void Start()
    {
        
    }

    void Update()
    {
        fTiempo += Time.deltaTime;
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
        if(tiempo >= 80)
        {
            lanzarOla_2 = true;
        }
        if (tiempo >= 200)
        {
            lanzarOla_2 = true;
        }
    }
}
