using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image barravida;

    LogicaEnemigos vitalidad;
    public float vidamax;
    public float vida_actual;

    private void Awake()
    {
        vitalidad = GetComponent<LogicaEnemigos>();
    }

    private void Start()
    {
        vidamax = vitalidad.vida;
        vida_actual = vidamax;
    } 

    public void vidactual()
    {
        barravida.fillAmount = (1 / vidamax) * vida_actual;
    }
}