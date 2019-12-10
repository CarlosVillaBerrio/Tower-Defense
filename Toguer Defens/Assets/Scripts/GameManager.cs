using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject stPeterBurger;
    public GameObject menuTorretas;
    public GameObject menuPause;
    public LogicaBotones[] botonesTorretas;
    public GameObject[] prefabsTorretas;
    public float cantidadRecursos;
    public Text numCantidadRecursos;
    public Text avisoRecursos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        avisoRecursos.gameObject.SetActive(false);
    }
}
