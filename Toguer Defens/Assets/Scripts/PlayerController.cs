using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public string selectableTag = "Selectable";
    public string torretaTag = "Torreta";
    public Material highlightMaterial;
    public Material defaultMaterial;

    Transform _selection;
    Transform plataformaT;
    public Transform torretica;

    public bool reiniciarSeleccion = false;
    AudioSource audioBoton;
    public AudioClip presionaBoton;

    private void Awake()
    {
        audioBoton = GetComponent<AudioSource>();
    }

    void Update()
    {
        SeleccionarPlataforma();
    }

    IEnumerator ReinicioSeleccion()
    {
        if (reiniciarSeleccion) StopCoroutine(ReinicioSeleccion());
        reiniciarSeleccion = true;
        yield return new WaitForSeconds(3f);
        reiniciarSeleccion = false;
    }

    void SeleccionarPlataforma()
    {

        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //if (reiniciarSeleccion == true) return;

            var selection = hit.transform;
            if (selection.CompareTag(selectableTag) && !reiniciarSeleccion)
            {
                plataformaT = selection;
                torretica = null;
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;

                    if (Input.GetMouseButtonDown(0))
                    {
                        GameManager.instance.menuTorretas.SetActive(true);
                        StartCoroutine(ReinicioSeleccion());
                    }
                }
                _selection = selection;
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    GameManager.instance.menuTorretas.SetActive(false);
                }
            }
        }

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(torretaTag))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    torretica = selection;
                    plataformaT = null;
                    GameManager.instance.menuTorretas.SetActive(true);
                }
            }
        }
    }

    public void VenderTorreta(int vende)
    {
        if(vende == 0)
        {
            if (torretica == null) return;
            var posicionGenerar = torretica.GetComponent<Transform>().gameObject;
            if (posicionGenerar.GetComponent<Torreta>() != null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
            {
                if (posicionGenerar.GetComponent<Torreta>().valorTorreta == 30)
                {
                    sumarRecursos(30);

                    Destroy(torretica.GetComponent<Transform>().gameObject);
                }
                if(posicionGenerar.GetComponent<Torreta>().valorTorreta == 50)
                {
                    sumarRecursos(50);
                    Destroy(posicionGenerar);
                }
                if(posicionGenerar.GetComponent<Torreta>().valorTorreta == 70)
                {
                    sumarRecursos(70);
                    Destroy(posicionGenerar);
                }
                if(posicionGenerar.GetComponent<Torreta>().valorTorreta == 90)
                {
                    sumarRecursos(90);
                    Destroy(posicionGenerar);
                }
                if(posicionGenerar.GetComponent<Torreta>().valorTorreta == 110)
                {
                    sumarRecursos(110);
                    Destroy(posicionGenerar);
                }
                if(posicionGenerar.GetComponent<Torreta>().valorTorreta == 150)
                {
                    sumarRecursos(150);
                    Destroy(posicionGenerar);
                }
            }
            GameManager.instance.menuTorretas.SetActive(false);

        }
    }

    void sumarRecursos(int cantidad)
    {
        GameManager.instance.cantidadRecursos += cantidad;
        GameManager.instance.numCantidadRecursos.text = GameManager.instance.cantidadRecursos.ToString();
    }

    void RestarRecursos(int cantidad)
    {
        GameManager.instance.cantidadRecursos -= cantidad;
        GameManager.instance.numCantidadRecursos.text = GameManager.instance.cantidadRecursos.ToString();
    }

   IEnumerator activaYDesactivaAviso()
    {
        if (GameManager.instance.avisoRecursos.gameObject == isActiveAndEnabled) StopCoroutine(activaYDesactivaAviso());
        GameManager.instance.avisoRecursos.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        GameManager.instance.avisoRecursos.gameObject.SetActive(false);
    }

    void SonarBoton()
    {
        audioBoton.Stop();
        audioBoton.PlayOneShot(presionaBoton);
    }

    public void CrearTorreta(int type)
    {
        if (plataformaT == null) return;
        if (type == 0)
        {
            SonarBoton();
            if (GameManager.instance.cantidadRecursos >= 30)
            {
                var posicionGenerar = plataformaT.GetComponentInChildren<Transform>();
                if (posicionGenerar.GetComponentInChildren<Torreta>() == null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
                {
                    RestarRecursos(30);
                    var leTurret = GameManager.instance.prefabsTorretas[type];
                    GameObject torretaInstanciada;
                    torretaInstanciada = Instantiate(leTurret, posicionGenerar.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    torretaInstanciada.transform.SetParent(posicionGenerar);
                    torretaInstanciada.name = "Torreta " + (type + 1);
                    torretaInstanciada.GetComponent<Torreta>().valorTorreta = 30;
                    GameManager.instance.menuTorretas.SetActive(false);
                }
            }
            else
            {
                StartCoroutine(activaYDesactivaAviso());
            }
        }
        if (type == 1)
        {
            SonarBoton();

            if (GameManager.instance.cantidadRecursos >= 50)
            {
                var posicionGenerar = plataformaT.GetComponentInChildren<Transform>();
                if (posicionGenerar.GetComponentInChildren<Torreta>() == null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
                {
                    RestarRecursos(50);

                    var leTurret = GameManager.instance.prefabsTorretas[type];
                    GameObject torretaInstanciada;
                    torretaInstanciada = Instantiate(leTurret, posicionGenerar.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    torretaInstanciada.transform.SetParent(posicionGenerar);
                    torretaInstanciada.name = "Torreta " + (type + 1);
                    torretaInstanciada.GetComponent<Torreta>().valorTorreta = 50;

                    GameManager.instance.menuTorretas.SetActive(false);

                }
            }
            else
            {
                StartCoroutine(activaYDesactivaAviso());
            }
        }
        if (type == 2)
        {
            SonarBoton();

            if (GameManager.instance.cantidadRecursos >= 70)
            {
                var posicionGenerar = plataformaT.GetComponentInChildren<Transform>();
                if (posicionGenerar.GetComponentInChildren<Torreta>() == null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
                {
                    RestarRecursos(70);

                    var leTurret = GameManager.instance.prefabsTorretas[type];
                    GameObject torretaInstanciada;
                    torretaInstanciada = Instantiate(leTurret, posicionGenerar.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    torretaInstanciada.transform.SetParent(posicionGenerar);
                    torretaInstanciada.name = "Torreta " + (type + 1);
                    torretaInstanciada.GetComponent<Torreta>().valorTorreta = 70;

                    GameManager.instance.menuTorretas.SetActive(false);

                }
            }
            else
            {
                StartCoroutine(activaYDesactivaAviso());
            }
        }
        if (type == 3)
        {
            SonarBoton();

            if (GameManager.instance.cantidadRecursos >= 90)
            {
                var posicionGenerar = plataformaT.GetComponentInChildren<Transform>();
                if (posicionGenerar.GetComponentInChildren<Torreta>() == null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
                {
                    RestarRecursos(90);

                    var leTurret = GameManager.instance.prefabsTorretas[type];
                    GameObject torretaInstanciada;
                    torretaInstanciada = Instantiate(leTurret, posicionGenerar.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    torretaInstanciada.transform.SetParent(posicionGenerar);
                    torretaInstanciada.name = "Torreta " + (type + 1);
                    torretaInstanciada.GetComponent<Torreta>().valorTorreta = 90;

                    GameManager.instance.menuTorretas.SetActive(false);

                }
            }
            else
            {
                StartCoroutine(activaYDesactivaAviso());
            }
        }
        if (type == 4)
        {
            SonarBoton();

            if (GameManager.instance.cantidadRecursos >= 110)
            {
                var posicionGenerar = plataformaT.GetComponentInChildren<Transform>();
                if (posicionGenerar.GetComponentInChildren<Torreta>() == null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
                {
                    RestarRecursos(110);

                    var leTurret = GameManager.instance.prefabsTorretas[type];
                    GameObject torretaInstanciada;
                    torretaInstanciada = Instantiate(leTurret, posicionGenerar.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    torretaInstanciada.transform.SetParent(posicionGenerar);
                    torretaInstanciada.name = "Torreta " + (type + 1);
                    torretaInstanciada.GetComponent<Torreta>().valorTorreta = 110;

                    GameManager.instance.menuTorretas.SetActive(false);

                }
            }
            else
            {
                StartCoroutine(activaYDesactivaAviso());
            }
        }
        if (type == 5)
        {
            SonarBoton();

            if (GameManager.instance.cantidadRecursos >= 150)
            {
                var posicionGenerar = plataformaT.GetComponentInChildren<Transform>();
                if (posicionGenerar.GetComponentInChildren<Torreta>() == null) // RESTRICCION: UNA SOLA TORRE EN LA PLATAFORMA
                {
                    RestarRecursos(150);

                    var leTurret = GameManager.instance.prefabsTorretas[type];
                    GameObject torretaInstanciada;
                    torretaInstanciada = Instantiate(leTurret, posicionGenerar.position + new Vector3(0, 2f, 0), Quaternion.identity);
                    torretaInstanciada.transform.SetParent(posicionGenerar);
                    torretaInstanciada.name = "Torreta " + (type + 1);
                    torretaInstanciada.GetComponent<Torreta>().valorTorreta = 150;

                    GameManager.instance.menuTorretas.SetActive(false);

                }
            }
            else
            {
                StartCoroutine(activaYDesactivaAviso());
            }
        }
    }
}