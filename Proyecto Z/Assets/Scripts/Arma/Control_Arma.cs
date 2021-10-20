using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animation))]

public class Control_Arma : MonoBehaviour
{
    public int i_balasCargador = 30;
    public int i_balasRecamara = 180;
    public int i_balasRecamaraCopia;
    public int i_dañoBala = 200;
    Text txt_municion;

    public float f_tiempoEntreDisparo = 0.01f;

    float f_temporizador = 0;

    public float f_tiempoInstaKill = 0;
    public float F_tiempoInstaKill { get => f_tiempoInstaKill; set => f_tiempoInstaKill = value; }

    Ray r_rayoDisparo;
    RaycastHit rh_hitDisparo;

    Animation a_animacionArma;
    Estados_Player2 estadosPlayer;
    public GameObject go_Fogonazo;
    public AudioClip ac_sonidoDisparo;
    public AudioClip ac_sonidoRecarga;
    Text txt_DuracionPowerUp;

    void Start()
    {
        estadosPlayer = GetComponentInParent<Estados_Player2>();
        txt_municion = GameObject.Find("Texto_Municion").GetComponent<Text>();
        a_animacionArma = GetComponent<Animation>();
        go_Fogonazo.SetActive(false);
        i_balasRecamaraCopia = i_balasRecamara;
        setTextoBalasCargador();
        txt_DuracionPowerUp = GameObject.Find("Duracion_PowerUp").GetComponent<Text>();
    }

    void Update()
    {
        if (!GetComponentInParent<Player_Gestor2>().b_creador)
            return;

        if (f_tiempoInstaKill > 0)
        {
            txt_DuracionPowerUp.text = ((int)f_tiempoInstaKill).ToString();
            f_tiempoInstaKill -= Time.deltaTime;
            i_dañoBala = 99999;
            GameObject.Find("Imagen_PowerUp").GetComponent<Image>().enabled = true;
        }
        else
        {
            txt_DuracionPowerUp.text = "";
            GameObject.Find("Imagen_PowerUp").GetComponent<Image>().enabled = false;
            i_dañoBala = 200;
        }

        f_temporizador += Time.deltaTime;

        if (estadosPlayer.B_disparar && f_temporizador >= f_tiempoEntreDisparo && !a_animacionArma.GetComponent<Animation>().IsPlaying("fire")
         && !a_animacionArma.GetComponent<Animation>().IsPlaying("reload") && i_balasCargador > 0)
        {
            GetComponentInParent<Player_Gestor2>().R_Aviso_Disparar();
            GetComponentInParent<Player_Gestor2>().R_Aviso_BalasCargador();
        }
        if (estadosPlayer.B_recargar && !a_animacionArma.GetComponent<Animation>().IsPlaying("fire")
            && !a_animacionArma.GetComponent<Animation>().IsPlaying("reload") && i_balasRecamara > 0 && i_balasCargador < 30)
        {
            GetComponentInParent<Player_Gestor2>().R_Aviso_Recargar();
            GetComponentInParent<Player_Gestor2>().R_Aviso_BalasCargador();
        }
        if (i_balasRecamara <= 0 && !a_animacionArma.GetComponent<Animation>().IsPlaying("fire")
            && !a_animacionArma.GetComponent<Animation>().IsPlaying("reload") && i_balasRecamara > 0)
        {
            GetComponentInParent<Player_Gestor2>().R_Aviso_Recargar();
            GetComponentInParent<Player_Gestor2>().R_Aviso_BalasCargador();
        }
        setTextoBalasCargador();
    }

    public void setTextoBalasCargador()
    {
        txt_municion.text = i_balasCargador.ToString() + " / " + i_balasRecamara.ToString();
    }

    public void Disparar()
    {
        if (!GetComponentInParent<Player_Gestor2>().b_creador)
            return;

        f_temporizador = 0f;
        Anim_Disparar();
        go_Fogonazo.SetActive(true);
        Invoke("Fogonazo", 0.05f);
        i_balasCargador--;

        r_rayoDisparo = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(r_rayoDisparo.origin, r_rayoDisparo.direction, out rh_hitDisparo))
        {
            Control_Zombie vidaZombie = rh_hitDisparo.collider.GetComponent<Control_Zombie>();
            if (vidaZombie != null)
            {
                GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().sumaPuntos(10);
                vidaZombie.RecibirDaño(i_dañoBala);
                print("Vida: " + vidaZombie.f_vidaZombie + "Daño: " + i_dañoBala);
            }
        }
    }

    public void Recargar()
    {
        if (i_balasRecamara == 0)
            return;

        GetComponentInParent<Player_Gestor2>().R_Aviso_BalasCargador();
        Anim_Recargar();

        if (i_balasRecamara > 0 && i_balasRecamara < 30)
        {
            i_balasCargador = i_balasRecamara;
        }
        else
        {
            if (i_balasRecamara >= 30)
            {
                i_balasRecamara = i_balasRecamara - (30 - i_balasCargador);
                i_balasCargador = 30;
            }
        }
    }

    private void Fogonazo()
    {
        go_Fogonazo.SetActive(false);
    }

    public void setMunicionMaxima()
    {
        i_balasRecamara = i_balasRecamaraCopia;
    }

    public void Anim_Disparar()
    {
        a_animacionArma.GetComponent<Animation>().Play("fire", PlayMode.StopAll);
        a_animacionArma.GetComponent<AudioSource>().PlayOneShot(ac_sonidoDisparo);
    }

    public void Anim_Recargar()
    {
        a_animacionArma.GetComponent<Animation>().Play("reload", PlayMode.StopAll);
        a_animacionArma.GetComponent<AudioSource>().PlayOneShot(ac_sonidoRecarga);
    }

}
