using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject[] prefabEnemigo = new GameObject[3];
    public Transform[] posicionesPrefabs = new Transform[3];
    int contador = 0;
    int tipoEnemigo = 0;
    int cantidadEnemigos = 0;
    private int nOla_1 = 10;
    bool generadaOla_1 = false;
    private int nOla_2 = 15;
    bool generadaOla_2 = false;
    private int nOla_3 = 20;
    bool generadaOla_3 = false;
    public float cadenciaGeneracion;
    bool puedeGenerar = true;
    public GameObject codigoTiempo;
    
    void Update()
    {
        generadorUpdate();
    }

    public void generadorUpdate()
    {
        if (!puedeGenerar) return;
        if (cantidadEnemigos < nOla_1 && !generadaOla_1) // PRIMERA OLA
        {
            puedeGenerar = false;
            StartCoroutine(rutinaGeneradora());
            if (cantidadEnemigos == nOla_1)
            {
                generadaOla_1 = true;
                cantidadEnemigos = 0;
            }
        }
        if (cantidadEnemigos < nOla_2 && !generadaOla_2 && generadaOla_1) // SEGUNDA OLA
        {

            if (codigoTiempo.GetComponent<ElTiempo>().lanzarOla_2 == true)
            {
                puedeGenerar = false;
                StartCoroutine(rutinaGeneradora());
                if (cantidadEnemigos == nOla_2)
                {
                    generadaOla_2 = true;
                    cantidadEnemigos = 0;

                }
            }            
        }
        if (cantidadEnemigos < nOla_3 && !generadaOla_3 && generadaOla_1 && generadaOla_2) // TERCERA OLA
        {
            if (codigoTiempo.GetComponent<ElTiempo>().lanzarOla_3)
            {
                puedeGenerar = false;
                StartCoroutine(rutinaGeneradora());
                if (cantidadEnemigos == nOla_3)
                {
                    generadaOla_3 = true;
                    cantidadEnemigos = 0;
                }
            }            
        }
    }

    IEnumerator rutinaGeneradora()
    {
        cantidadEnemigos++;
        yield return new WaitForSeconds(cadenciaGeneracion);
        generaEnemigos();
        puedeGenerar = true;
    }

    public void generaEnemigos()
    {
        GameObject prefabGenerado;
        contador = Random.Range(0, 3);
        tipoEnemigo = Random.Range(0, 3);
        prefabGenerado = Instantiate(prefabEnemigo[tipoEnemigo], posicionesPrefabs[contador].position, Quaternion.identity);
        prefabGenerado.transform.SetParent(posicionesPrefabs[contador]);
        prefabGenerado.name = "LaCucaracha";
    }
}
