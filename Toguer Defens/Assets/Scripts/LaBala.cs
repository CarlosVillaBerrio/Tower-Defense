using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaBala : MonoBehaviour
{
    public Torreta torreta;
    public float daño;

    private void Awake()
    {
        torreta = GetComponentInParent<Torreta>();
        daño = torreta.damage;
    }

    private void Update()
    {
        Disparo();
    }

    void Disparo()
    {
        torreta.CalcularTrayectoria();
        gameObject.transform.position += torreta.direction * torreta.velocidadB * 0.9f;
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LogicaEnemigos>())
        {
            gameObject.SetActive(false);

            Destroy(gameObject, 0.2f);
        }
        if (other.tag == "Terreno")
        {
            gameObject.SetActive(false);

            Destroy(gameObject, 0.2f);
        }
    }
}
