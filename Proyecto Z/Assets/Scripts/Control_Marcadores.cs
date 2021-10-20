using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Control_Marcadores : MonoBehaviour
{
    Text txt_rondas;
    Text txt_bajas;
    Text txt_puntos;

    void Start()
    {
        txt_rondas = GameObject.Find("Texto_Ronda").GetComponent<Text>();
        txt_bajas = GameObject.Find("Texto_Baja").GetComponent<Text>();
        txt_puntos = GameObject.Find("Texto_Punto").GetComponent<Text>();
    }

    void Update()
    {
        txt_rondas.text = "Has sobrevivido  " + GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().i_ronda.ToString() + "  rondas.";
        txt_bajas.text = GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().i_bajas.ToString();
        txt_puntos.text = GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().i_puntosTotales.ToString();
    }

}
