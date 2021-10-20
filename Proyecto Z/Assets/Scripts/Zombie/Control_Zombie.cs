using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NavMeshAgent))]

public class Control_Zombie : MonoBehaviour
{

    public float f_vidaZombie = 20;
    public float F_vidaZombie { get => f_vidaZombie; set => f_vidaZombie = value; }

    int i_ataque = 50;
    public float f_velocidad = 2f;
    float f_distancia;
    bool b_ataqueZombie = false;
    int i_muerteRandom;
    bool b_sonido = true;

    int cont = 1;

    GameObject controladorPowerUps;
    Control_PowerUps controlPowerUps;
    Control_Rondas controlRondas;
    GameObject go_playerCercano;
    Animator a_animacionZombie;
    public AudioClip ac_sonidoMuerte;
    NavMeshAgent nav;

    void Start()
    {
        controladorPowerUps = GameObject.Find("Controlador_PowerUps");
        controlPowerUps = GameObject.Find("Controlador_PowerUps").GetComponent<Control_PowerUps>();
        controlRondas = GameObject.Find("Controlador_Rondas").GetComponent<Control_Rondas>();
        i_muerteRandom = Random.Range(1, 3);
        a_animacionZombie = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        nav.enabled = true;

    }

    void Update()
    {

        GameObject[] go_player = GameObject.FindGameObjectsWithTag("Player");

        //Se obtiene el jugador más cercano al Zombie.
        go_playerCercano = getJugadorCercano(go_player);

        //Distancia entre el Player y el Zombie.
        f_distancia = Vector3.Distance(transform.position, go_playerCercano.transform.position);

        //Seguimiento del Player.
        if (f_vidaZombie >= 1)
            nav.destination = go_playerCercano.transform.position;

        //Distancia de ataque.
        if (f_distancia > 2.0f)
        {
            nav.speed = f_velocidad;
            a_animacionZombie.SetBool("ataque", false);
        }

        //Ataque del Zombie.
        if (f_distancia < nav.stoppingDistance && b_ataqueZombie == false)
        {
            nav.speed = 0;
            a_animacionZombie.SetBool("ataque", true);
            Invoke("Atacar", 0.8f); //Tiempo que tarda en atacar.
            b_ataqueZombie = true;
        }

        //Muerte del Zombie.
        if (f_vidaZombie <= 0)
        {
            cont--;
            if (cont == 0)
            {
                a_animacionZombie.SetBool("reset", false);
                nav.speed = 0.0f;
                i_ataque = 0;

                GetComponent<CharacterController>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                GameObject.Find("Controlador_Rondas").GetComponent<Control_Rondas>().ZombieMuerto();
                GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().sumaPuntos(50);
                SoltarPowerUp();
                Invoke("Morir", 4.0f); //Tiempo que tarda en desaparecer cuando muere.

                GetComponent<R_Zombie_Gestor>().R_Aviso_AnimMuerte();
            }
        }
        if (b_sonido == true && f_vidaZombie <= 0)
        {
            b_sonido = false;
            GetComponent<AudioSource>().PlayOneShot(ac_sonidoMuerte);

            if (f_velocidad < (go_playerCercano.GetComponent<Vida_Player>().f_vidaPlayer - 0.5f))
            {
                f_velocidad += 0.02f;
            }

        }
    }

    public void Atacar()
    {
        if (!go_playerCercano.GetComponent<Player_Gestor2>().b_creador)
            return;

        Vida_Player vidaPlayer = go_playerCercano.GetComponent<Vida_Player>();

        Invoke("CancelarAtaque", 1.5f);
        if (f_distancia <= 2.5f && b_ataqueZombie == true)
        {
            go_playerCercano.GetComponent<Player_Gestor2>().R_Aviso_RecibirDaño(i_ataque);
        }
    }

    public void Morir()
    {
        GetComponent<R_Zombie_Gestor>().R_Aviso_Morir();
    }

    public void Animacion_Muerte()
    {
        if (i_muerteRandom == 1)
        {
            a_animacionZombie.SetBool("muerte1", true);
        }

        if (i_muerteRandom == 2)
        {
            a_animacionZombie.SetBool("muerte2", true);
        }
    }

    public void CancelarAtaque()
    {
        b_ataqueZombie = false;
    }

    public void RecibirDaño(int i_dañoPorDisparo)
    {
        //if (!GetComponent<R_Zombie_Gestor>().networkObject.IsOwner)
        //return;

        f_vidaZombie -= i_dañoPorDisparo;
    }

    public void setIncrementoVida(int i_ronda)
    {
        if (i_ronda <= 9)
        {
            f_vidaZombie += 100;
        }
        else
        {
            f_vidaZombie *= 1.1f;
        }
    }

    public GameObject getJugadorCercano(GameObject[] go_player)
    {
        GameObject go_playerCercano = go_player[0];
        for (int i = 1; i < go_player.Length; i++)
        {
            if (Vector3.Distance(transform.position, go_playerCercano.transform.position) >
                Vector3.Distance(transform.position, go_player[i].transform.position))
            {
                go_playerCercano = go_player[i];
            }
        }
        return go_playerCercano;
    }

    void SoltarPowerUp()
    {
        if (controlRondas.I_ronda >= 0)
        {
            if (controladorPowerUps.GetComponent<Control_PowerUps>().I_num1 == controlRondas.I_zombiesVivos ||
                controladorPowerUps.GetComponent<Control_PowerUps>().I_num2 == controlRondas.I_zombiesVivos)
            {
                NetworkManager.Instance.InstantiateR_PowerUp(Random.Range(0, 3), transform.position + new Vector3(0, 0.8f, 0), transform.rotation);
            }
        }

    }

}