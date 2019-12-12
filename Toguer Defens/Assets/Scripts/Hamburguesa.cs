using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
                SceneManager.LoadScene("FantasyLose");
            }
        }
    }
}
