using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Control_Rondas : MonoBehaviour
{
    Spawn2 spawn;
    GameObject[] go_spawns;

    Control_PowerUps controlPowerUps;

    public GameObject go_zombie;

    public int i_ronda = 1;
    public int I_ronda { get => i_ronda; set => i_ronda = value; }

    public bool b_siguienteRonda = true;

    public int i_zombiesVivos;
    public int I_zombiesVivos { get => i_zombiesVivos; set => i_zombiesVivos = value; }

    public int i_contZombiesMuertos = 0;
    public int I_contZombiesMuertos { get => i_contZombiesMuertos; set => i_contZombiesMuertos = value; }

    public int i_zombiesPorRonda = 6;
    public int I_zombiesPorRonda { get => i_zombiesPorRonda; set => i_zombiesPorRonda = value; }

    public int i_zombiesPorInstanciar;
    public int I_zombiesPorInstanciar { get => i_zombiesPorInstanciar; set => i_zombiesPorInstanciar = value; }

    Text txt_ronda;
    public AudioClip ac_nuevaRonda;

    void Start()
    {

        go_spawns = GameObject.FindGameObjectsWithTag("Respawn");
        spawn = GameObject.Find("Spawn").GetComponent<Spawn2>();
        i_zombiesVivos = i_zombiesPorRonda;
        controlPowerUps = GameObject.Find("Controlador_PowerUps").GetComponent<Control_PowerUps>();
        controlPowerUps.RandomizarNumeros(i_zombiesVivos);
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Gestor2>().b_creador)
            return;

        txt_ronda = GameObject.Find("Texto_Ronda").GetComponent<Text>();

        if (i_zombiesVivos == 0)
        {
            b_siguienteRonda = true;
        }

        if (i_zombiesVivos == 0 && b_siguienteRonda == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Gestor2>().R_Aviso_Ronda();
        }
        txt_ronda.text = i_ronda.ToString();

        setZombiesPorInstanciar();
    }

    public void NuevaRonda()
    {
        i_ronda++;
        b_siguienteRonda = false;


        if (i_ronda < 64)
        {
            foreach (GameObject spawn in go_spawns)
            {
                spawn.GetComponent<Spawn2>().setSpawnDelay();
            }
        }

        GetComponent<AudioSource>().PlayOneShot(ac_nuevaRonda);

        if (i_ronda >= 12)
        {
            i_zombiesPorRonda = (int)Math.Round(0.0842 * Math.Pow(i_ronda, 2) + 0.1954 * i_ronda + 22.05);
        }
        else
        {
            ZombiesRondasIniciales();
        }

        i_zombiesVivos = i_zombiesPorRonda;
        go_zombie.GetComponent<Control_Zombie>().setIncrementoVida(i_ronda);
        controlPowerUps.RandomizarNumeros(i_zombiesVivos);

        foreach (GameObject spawn in go_spawns)
        {
            spawn.GetComponent<Spawn2>().setTimer(-15.0f);
        }

    }

    public void ZombieMuerto()
    {
        i_zombiesVivos--;
        i_contZombiesMuertos += 1;
        spawn.DisminuirZombiesInstanciados();
    }

    public void ZombiesRondasIniciales()
    {
        switch (i_ronda)
        {
            case 2:
                i_zombiesPorRonda = 8;
                break;
            case 3:
                i_zombiesPorRonda = 13;
                break;
            case 4:
                i_zombiesPorRonda = 18;
                break;
            case 5:
                i_zombiesPorRonda = 24;
                break;
            case 6:
                i_zombiesPorRonda = 27;
                break;
            case 7:
            case 8:
                i_zombiesPorRonda = 28;
                break;
            case 9:
                i_zombiesPorRonda = 29;
                break;
            case 10:
                i_zombiesPorRonda = 33;
                break;
            case 11:
                i_zombiesPorRonda = 34;
                break;
            case 12:
                i_zombiesPorRonda = 36;
                break;
        }
    }

    void setZombiesPorInstanciar()
    {
        if (i_zombiesPorRonda >= 24 && i_zombiesPorInstanciar == 0)
        {
            i_zombiesPorInstanciar = 24;
            i_zombiesPorRonda -= i_zombiesPorInstanciar;
        }

        if (i_zombiesPorRonda < 24 && i_zombiesPorInstanciar == 0)
        {
            i_zombiesPorInstanciar = i_zombiesPorRonda;
            i_zombiesPorRonda = 0;
        }

    }


}