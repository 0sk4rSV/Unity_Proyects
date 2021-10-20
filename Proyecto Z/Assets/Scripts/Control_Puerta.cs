using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]

public class Control_Puerta : MonoBehaviour
{
    Estados_Player2 estadosPlayer;
    Puntos puntosPlayer;
    Text txt_abrirPuerta;

    public AudioClip ac_sonidoCompra;

    bool b_puertaAbierta = false;
    public float f_rotacionPuerta;
    public int i_costePuerta;

    // Start is called before the first frame update
    void Start()
    {
        estadosPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Estados_Player2>();
        puntosPlayer = GameObject.Find("Controlador_Puntos").GetComponent<Puntos>();
        txt_abrirPuerta = GameObject.Find("Texto_Mensaje").GetComponent<Text>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Gestor2>().b_creador)
            return;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Gestor2>().R_Aviso_Mensaje_AbrirPuerta();

        if (estadosPlayer.B_interactuar && puntosPlayer.I_puntosTotales >= i_costePuerta && b_puertaAbierta == false)
        {
            puntosPlayer.restaPuntos(i_costePuerta);
            GetComponent<R_Sinc_Objeto_Generico>().R_Aviso_AbrirPuerta();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        txt_abrirPuerta.text = "";
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Estados_Player2>().B_android && Input.touchSupported)
        {
            GameObject.Find("Boton_Interactuar").GetComponent<Image>().enabled = false;
        }
    }

    public void AbrirPuerta()
    {
        GetComponent<AudioSource>().PlayOneShot(ac_sonidoCompra);
        //GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        transform.Rotate(0.0f, f_rotacionPuerta, 0.0f);

        txt_abrirPuerta.text = "";
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Estados_Player2>().B_android && Input.touchSupported)
        {
            GameObject.Find("Boton_Interactuar").GetComponent<Image>().enabled = false;
        }

    }

    public void MensajePuerta()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Estados_Player2>().B_android && !Input.touchSupported)
        {
            txt_abrirPuerta.text = "Pulsa E para abrir puerta [Coste: " + i_costePuerta.ToString() + "].";
        }
        else
        {
            txt_abrirPuerta.text = "Pulsa para abrir puerta [Coste: " + i_costePuerta.ToString() + "].";
            GameObject.Find("Boton_Interactuar").GetComponent<Image>().enabled = true;
        }
    }

}