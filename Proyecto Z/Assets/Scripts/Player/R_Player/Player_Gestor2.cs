using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class Player_Gestor2 : R_PlayerBehavior
{
    public bool b_creador = false;

    Vector3 v3_networkObject_position = Vector3.zero;
    Vector3 v3_velocitat = Vector3.zero;
    float f_temps_xarxa = 0f;

    protected override void NetworkStart()
    {
        GetComponentInChildren<Camera>().enabled = false;
        GetComponentInChildren<AudioListener>().enabled = false;

        base.NetworkStart();

        b_creador = networkObject.IsOwner; //Indica si el cliente es el creador del personaje, al iniciar el jeugo.

        v3_networkObject_position = transform.position;

        if (b_creador)
        {
            GetComponentInChildren<Camera>().enabled = true;
            GetComponentInChildren<AudioListener>().enabled = true;
        }

        if (networkObject.IsServer && !networkObject.IsOwner)
        {
            networkObject.TakeOwnership(); //En este momento el cliente es el propietario del objeto y el cliente ya no.
        }

    }

    void Update()
    {
        if (networkObject == null)
            return;

        if (!networkObject.IsOwner)
        {
            f_temps_xarxa += Time.deltaTime;
            if (v3_networkObject_position != networkObject.position)
            {
                v3_velocitat =
                    Vector3.Normalize(networkObject.position - transform.position) *
                    Vector3.Distance(networkObject.position, transform.position) /
                    f_temps_xarxa;

                f_temps_xarxa = 0f;
                v3_networkObject_position = networkObject.position;

            }

            float f_distancia = Vector3.Distance(networkObject.position, transform.position);
            if (f_distancia > 2f)
                transform.position = networkObject.position;
            else if (f_distancia > 0.05f)
            {
                transform.position += v3_velocitat * Time.deltaTime;
                transform.rotation = networkObject.rotationX;
                gameObject.GetComponent<Movimiento_Personaje2>().MoverManos(networkObject.rotationY);

                return;
            }
        }
        networkObject.position = transform.position;
        networkObject.rotationX = transform.rotation;
        networkObject.rotationY = GetComponent<Movimiento_Personaje2>().go_manos.transform.localEulerAngles;

    }

    //RPC movimiento y rotacion personaje.
    public void R_Aviso_Enviar_Estados(bool b_adelante, bool b_izquierda, bool b_atras, bool b_derecha, bool b_saltar, bool b_correr, bool b_interactuar, bool b_disparar, bool b_recargar)
    {
        networkObject.SendRpc(RPC_R__ENVIAR__ESTADOS, Receivers.Owner, b_adelante, b_izquierda, b_atras, b_derecha, b_saltar, b_correr, b_interactuar, b_disparar, b_recargar);
    }

    public override void R_Enviar_Estados(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        GetComponent<Estados_Player2>().B_adelante = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_izquierda = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_atras = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_derecha = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_saltar = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_correr = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_interactuar = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_disparar = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_recargar = args.GetNext<bool>();
    }

    //RPC rotacion cámara.
    public void R_Aviso_Enviar_Rotacion(bool b_rotarPlayer, bool b_rotarCamara, float f_rotacionX, float f_rotacionY)
    {
        networkObject.SendRpc(RPC_R__ENVIAR__ROTACION, Receivers.Owner, b_rotarPlayer, b_rotarCamara, f_rotacionX, f_rotacionY);
    }

    public override void R_Enviar_Rotacion(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        GetComponent<Estados_Player2>().B_rotarPlayer = args.GetNext<bool>();
        GetComponent<Estados_Player2>().B_rotarCamara = args.GetNext<bool>();
        GetComponent<Estados_Player2>().F_mouse_x = args.GetNext<float>();
        GetComponent<Estados_Player2>().F_mouse_y = args.GetNext<float>();
    }

    //NO SE USA RPC cambiar texto y slider vida.
    public void R_Aviso_VidaPlayer(float f_vidaPlayer)
    {
        networkObject.SendRpc(RPC_R__VIDA_PLAYER, Receivers.All, f_vidaPlayer);
    }

    public override void R_VidaPlayer(RpcArgs args)
    {
        GetComponent<Vida_Player>().setTextoVida(args.GetNext<float>());
    }

    //RPC disparo arma.
    public void R_Aviso_Disparar()
    {
        networkObject.SendRpc(RPC_R__DISPARAR, Receivers.All);
    }

    public override void R_Disparar(RpcArgs args)
    {
        GetComponentInChildren<Control_Arma>().Disparar();
    }

    //RPC animación disparo del arma.
    public void R_Aviso_AnimDisparar()
    {
        networkObject.SendRpc(RPC_R__ANIM_DISPARAR, Receivers.All);
    }

    public override void R_AnimDisparar(RpcArgs args)
    {
        GetComponentInChildren<Control_Arma>().Anim_Disparar();
    }

    //RPC recargar arma.
    public void R_Aviso_Recargar()
    {
        networkObject.SendRpc(RPC_R__RECARGAR, Receivers.All);
    }

    public override void R_Recargar(RpcArgs args)
    {
        GetComponentInChildren<Control_Arma>().Recargar();
    }

    //RPC animación recarga del arma.
    public void R_Aviso_AnimRecargar()
    {
        networkObject.SendRpc(RPC_R__ANIM_RECARGAR, Receivers.All);
    }

    public override void R_AnimRecargar(RpcArgs args)
    {
        GetComponentInChildren<Control_Arma>().Anim_Recargar();
    }

    //RPC cambiar texto balas cargador.
    public void R_Aviso_BalasCargador()
    {
        networkObject.SendRpc(RPC_R__BALAS_CARGADOR, Receivers.Owner);
    }

    public override void R_BalasCargador(RpcArgs args)
    {
        GetComponentInChildren<Control_Arma>().setTextoBalasCargador();
    }

    //RPC recibir daño.
    public void R_Aviso_RecibirDaño(int i_danyo)
    {
        networkObject.SendRpc(RPC_R__RECIBIR_DANYO, Receivers.All, i_danyo);
    }
    public override void R_RecibirDanyo(RpcArgs args)
    {
        GetComponent<Vida_Player>().RecibirDaño(args.GetNext<int>());
    }

    //RPC mensaje abrir puerta.
    public void R_Aviso_Mensaje_AbrirPuerta()
    {
        networkObject.SendRpc(RPC_R__MENSAJE__ABRIR_PUERTA, Receivers.Owner);
    }
    public override void R_Mensaje_AbrirPuerta(RpcArgs args)
    {
        GameObject.FindGameObjectWithTag("Puerta").GetComponent<Control_Puerta>().MensajePuerta();
    }

    //RPC mensaje abrir puerta.
    public void R_Aviso_Ronda()
    {
        networkObject.SendRpc(RPC_R__RONDA, Receivers.All);
    }
    public override void R_Ronda(RpcArgs args)
    {
        GameObject.Find("Controlador_Rondas").GetComponent<Control_Rondas>().NuevaRonda();
    }

}
