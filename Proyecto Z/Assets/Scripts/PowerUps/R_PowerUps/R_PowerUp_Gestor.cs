using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;

public class R_PowerUp_Gestor : R_PowerUpBehavior
{
    protected override void NetworkStart()
    {
        base.NetworkStart();

        if (!networkObject.IsOwner)
            return;

        networkObject.SendRpc(RPC_R__POSICION__INICIAL, Receivers.Others,
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

    public override void R_Posicion_Inicial(RpcArgs args)
    {
        //throw new System.NotImplementedException();
        networkObject.position = args.GetNext<Vector3>();
        networkObject.rotation = args.GetNext<Quaternion>();
        networkObject.SnapInterpolations();
    }

    //RPC Destroy PowerUp.
    public void R_Aviso_Destruir_PowerUp()
    {
        networkObject.SendRpc(RPC_R__DESTRUIR__POWER_UP, Receivers.All);
    }

    public override void R_Destruir_PowerUp(RpcArgs args)
    {
        networkObject.Destroy(0);
    }

}
