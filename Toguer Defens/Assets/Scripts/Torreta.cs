using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public float damage;
    public GameObject enemigoVisto;
    public bool enemigoFijado = false;
    public GameObject particleBullet;
    public Vector3 dInsecto;
    public Vector3 direction;
    public float distanciaInsecto;
    public float velocidadB = 80f;
    public Transform origenBala;
    public bool puedeDisparar = true;
    public int valorTorreta;
    public AudioClip sonidoGato;
    AudioSource audioGato;
    public ParticleSystem fogonazo;

    private void Awake()
    {
        audioGato = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(ActivarDisparos());
    }

    void Update()
    {

    }

    IEnumerator ActivarDisparos()
    {
        while (true)
        {
            EnemigoDetectado();
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator esperaOtroTiro()
    {

        yield return new WaitForSeconds(2f);
        puedeDisparar = true;
        enemigoFijado = false;

    }

    void EnemigoDetectado()
    {
        if (enemigoVisto == null) return;
        else
        {
            if (enemigoVisto.GetComponent<LogicaEnemigos>().estadoVivo == true)
            {
                transform.LookAt(enemigoVisto.transform);
                AtacarEnemigo();
            }
        }
    }

    public void CalcularTrayectoria()
    {
        if (enemigoVisto == null) return;
        dInsecto = enemigoVisto.transform.position - origenBala.position;
        distanciaInsecto = dInsecto.magnitude;
        direction = Vector3.Normalize(dInsecto);        
    }

    void sonarGato()
    {
        audioGato.Stop();
        audioGato.PlayOneShot(sonidoGato);
    }

    void activarFogonazo()
    {
        fogonazo.Stop();
        fogonazo.Play();
    }

    void AtacarEnemigo()
    {
        if (!puedeDisparar) return;
        GameObject laBala;
        laBala = Instantiate(particleBullet, origenBala);
        laBala.transform.localPosition = new Vector3(0, 0, 0);
        puedeDisparar = false;
        sonarGato();
        activarFogonazo();

        if (puedeDisparar == false)
        {
            StartCoroutine(esperaOtroTiro());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LogicaEnemigos>() && enemigoFijado == false)
        {
            enemigoVisto = other.GetComponent<LogicaEnemigos>().gameObject;
            enemigoFijado = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<LogicaEnemigos>() && enemigoFijado == false)
        {
            enemigoVisto = other.GetComponent<LogicaEnemigos>().gameObject;
            enemigoFijado = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LogicaEnemigos>())
        {
            enemigoFijado = false;
        }
    }
}
