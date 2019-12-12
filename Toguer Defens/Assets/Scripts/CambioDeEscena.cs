using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeEscena : MonoBehaviour
{
    public string nombreEscena;
    void Start()
    {
        StartCoroutine(CargarEscena());
    }

    IEnumerator CargarEscena()
    {
    yield return new WaitForSeconds(10f);
    SceneManager.LoadScene(nombreEscena);
    }
}
