using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Zombie_Gestor : R_ZombieBehavior
{
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsOwner)
            return;

        networkObject.SendRpc(RPC_POSICION__INICIAL, Receivers.Others,
            transform.position, transform.rotation);
        //GetComponent<Renderer>().enabled = true;
    }

    void Update()
    {
        if (networkObject == null)
            return;


        if (!networkObject.IsOwner)
        {
            transform.position = networkObject.position;
            transform.rotation = networkObject.rotation;
            return;
        }


        networkObject.position = transform.position;
        networkObject.rotation = transform.rotation;

    }
    public override void Posicion_Inicial(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        networkObject.position = args.GetNext<Vector3>();
        networkObject.rotation = args.GetNext<Quaternion>();
        networkObject.SnapInterpolations();
        //GetComponent<Renderer>().enabled = true;
    }

    //RPC animación muerte del zombie.
    public void R_Aviso_AnimMuerte()
    {
        networkObject.SendRpc(RPC_R__ANIM__MUERTE, Receivers.All);
    }

    public override void R_Anim_Muerte(RpcArgs args)
    {
        GetComponent<Control_Zombie>().Animacion_Muerte();
    }

    //RPC muerte del zombie.
    public void R_Aviso_Morir()
    {
        networkObject.SendRpc(RPC_R__MUERTE__ZOMBIE, Receivers.All);
    }

    public override void R_Muerte_Zombie(RpcArgs args)
    {
        networkObject.Destroy(0);
    }


    public void Aviso_Atacar()
    {
        networkObject.SendRpc(RPC_ATACAR, Receivers.All);
    }

    public override void Atacar(RpcArgs args)
    {
        GetComponent<Control_Zombie>().Animacion_Muerte();
    }


    public void Aviso_CancelarAtaque()
    {
        networkObject.SendRpc(RPC_CANCELAR_ATAQUE, Receivers.All);
    }

    public override void CancelarAtaque(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        GetComponent<Control_Zombie>().CancelarAtaque();
    }

    public void Aviso_RecibirDaño(int i_daño)
    {
        networkObject.SendRpc(RPC_RECIBIR_DAÑO, Receivers.All, i_daño);
    }

    public override void RecibirDaño(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        GetComponent<Control_Zombie>().RecibirDaño(args.GetNext<int>());

    }
  
}
