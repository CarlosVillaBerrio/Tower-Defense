using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hamburguesa : MonoBehaviour
{
    float vidaMax = 10;
    float vidaActual = 10;

    public Image barravida;

    public void VidaEstado()
    {
        barravida.fillAmount = (1 / vidaMax) * vidaActual;
    }

    private void Start()
    {
        vidaActual = vidaMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LogicaEnemigos>())
        {
            vidaActual--;
            VidaEstado();
            if(vidaActual <= 0)
            {
                vidaActual = 0;
                Destroy(gameObject, 1.5f);
            }
        }
    }
}
