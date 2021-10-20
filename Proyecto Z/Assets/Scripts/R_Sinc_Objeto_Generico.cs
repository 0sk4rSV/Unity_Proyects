using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class R_Sinc_Objeto_Generico : R_ObjetoGenericoBehavior
{
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsOwner)
            return;

        networkObject.SendRpc(RPC_POSICION__INICIAL, Receivers.Others,
            transform.position, transform.rotation);
        GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
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
        GetComponent<Renderer>().enabled = true;
    }

    //RPC abrir puerta.
    public void R_Aviso_AbrirPuerta()
    {
        networkObject.SendRpc(RPC_R__ABRIR_PUERTA, Receivers.Owner);
    }

    public override void R_AbrirPuerta(RpcArgs args)
    {
        GetComponent<Control_Puerta>().AbrirPuerta();
    }

}
