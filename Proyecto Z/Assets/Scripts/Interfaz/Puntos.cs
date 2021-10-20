using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{
    Text txt_puntos;

    public int i_puntosTotales = 500;
    public int i_ronda = 0;
    public int i_bajas = 0;
    public int I_puntosTotales { get => i_puntosTotales; set => i_puntosTotales = value; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        txt_puntos = GameObject.Find("Texto_Puntos").GetComponent<Text>();
        actualizaPuntos();
    }
    // Update is called once per frame

    public void sumaPuntos(int i_puntos)
    {
        i_puntosTotales += i_puntos;
        actualizaPuntos();
    }
    public void restaPuntos(int i_puntos)
    {
        i_puntosTotales -= i_puntos;
        actualizaPuntos();
    }

    public void actualizaPuntos()
    {
        txt_puntos.text = i_puntosTotales.ToString();
    }

    public void getMarcadores()
    {
        i_ronda = GameObject.Find("Controlador_Rondas").GetComponent<Control_Rondas>().I_ronda - 1;
        i_bajas = GameObject.Find("Controlador_Rondas").GetComponent<Control_Rondas>().I_contZombiesMuertos;
    }

}
