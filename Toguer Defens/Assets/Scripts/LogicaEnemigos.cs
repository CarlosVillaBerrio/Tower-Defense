using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class LogicaEnemigos : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool estadoVivo;
    BarraVida barraVida;
    public float vida;

    private void Awake()
    {
        barraVida = GetComponent<BarraVida>();
    }

    void Start()
    {
        estadoVivo = true;
    }

    void Update()
    {
        if(transform.localPosition != GameManager.instance.stPeterBurger.transform.position)
        {
            agent.SetDestination(GameManager.instance.stPeterBurger.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LaBala>())
        {
            barraVida.vida_actual -= other.GetComponent<LaBala>().daño;
            barraVida.vidactual();
            if(barraVida.vida_actual <= 0)
            {
                barraVida.vida_actual = 0;
                estadoVivo = false;
                gameObject.SetActive(false);
                GameManager.instance.cantidadRecursos += 10;
                Destroy(gameObject, 0.4f);
            }            
        }
        if (other.GetComponent<Hamburguesa>())
        {
            barraVida.vida_actual = 0;
            estadoVivo = false;
            gameObject.SetActive(false);
            Destroy(gameObject, 0.4f);
        }
    }
}
