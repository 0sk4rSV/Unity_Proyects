using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System;

public class R_Desconectar_Gestor : R_DesconectaBehavior
{
    bool b_conectado = false;
    bool b_player2 = false;
    float f_contador = 5.0f;

    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsServer)
        {
            networkObject.SendRpc(RPC_R__P2__CONECTADO, Receivers.All, true);
        }
    }

    public void Update()
    {
        if (b_player2)
        {
            networkObject.SendRpc(RPC_R__DESCONECTAR, Receivers.Others, true);
            f_contador -= Time.deltaTime;
            if (f_contador <= 0)
            {
                if (!b_conectado)
                {
                    Application.Quit();
                }
                f_contador = 5.0f;
                b_conectado = false;
            }
        }
    }

    //RPC desconexión jugador.
    public override void R_Desconectar(RpcArgs args)
    {
        b_conectado = args.GetNext<bool>();
    }

    public override void R_P2_Conectado(RpcArgs args)
    {
        b_player2 = args.GetNext<bool>();
    }
}
