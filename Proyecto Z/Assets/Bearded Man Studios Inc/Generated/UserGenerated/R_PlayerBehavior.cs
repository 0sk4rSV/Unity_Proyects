using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"bool\", \"bool\", \"bool\", \"bool\", \"bool\", \"bool\", \"bool\", \"bool\", \"bool\"][\"bool\", \"bool\", \"float\", \"float\"][\"float\"][][][][][][\"int\"][][]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"b_adelante\", \"b_izquierda\", \"b_atras\", \"b_derecha\", \"b_saltar\", \"b_correr\", \"b_interactuar\", \"b_disparar\", \"b_recargar\"][\"b_rotarPlayer\", \"b_rotarCamara\", \"f_rotacionX\", \"f_rotacionY\"][\"f_vidaPlayer\"][][][][][][\"i_danyo\"][][]]")]
	public abstract partial class R_PlayerBehavior : NetworkBehavior
	{
		public const byte RPC_R__ENVIAR__ESTADOS = 0 + 5;
		public const byte RPC_R__ENVIAR__ROTACION = 1 + 5;
		public const byte RPC_R__VIDA_PLAYER = 2 + 5;
		public const byte RPC_R__DISPARAR = 3 + 5;
		public const byte RPC_R__RECARGAR = 4 + 5;
		public const byte RPC_R__ANIM_DISPARAR = 5 + 5;
		public const byte RPC_R__ANIM_RECARGAR = 6 + 5;
		public const byte RPC_R__BALAS_CARGADOR = 7 + 5;
		public const byte RPC_R__RECIBIR_DANYO = 8 + 5;
		public const byte RPC_R__MENSAJE__ABRIR_PUERTA = 9 + 5;
		public const byte RPC_R__RONDA = 10 + 5;
		
		public R_PlayerNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (R_PlayerNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("R_Enviar_Estados", R_Enviar_Estados, typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool));
			networkObject.RegisterRpc("R_Enviar_Rotacion", R_Enviar_Rotacion, typeof(bool), typeof(bool), typeof(float), typeof(float));
			networkObject.RegisterRpc("R_VidaPlayer", R_VidaPlayer, typeof(float));
			networkObject.RegisterRpc("R_Disparar", R_Disparar);
			networkObject.RegisterRpc("R_Recargar", R_Recargar);
			networkObject.RegisterRpc("R_AnimDisparar", R_AnimDisparar);
			networkObject.RegisterRpc("R_AnimRecargar", R_AnimRecargar);
			networkObject.RegisterRpc("R_BalasCargador", R_BalasCargador);
			networkObject.RegisterRpc("R_RecibirDanyo", R_RecibirDanyo, typeof(int));
			networkObject.RegisterRpc("R_Mensaje_AbrirPuerta", R_Mensaje_AbrirPuerta);
			networkObject.RegisterRpc("R_Ronda", R_Ronda);

			networkObject.onDestroy += DestroyGameObject;

			if (!obj.IsOwner)
			{
				if (!skipAttachIds.ContainsKey(obj.NetworkId)){
					uint newId = obj.NetworkId + 1;
					ProcessOthers(gameObject.transform, ref newId);
				}
				else
					skipAttachIds.Remove(obj.NetworkId);
			}

			if (obj.Metadata != null)
			{
				byte transformFlags = obj.Metadata[0];

				if (transformFlags != 0)
				{
					BMSByte metadataTransform = new BMSByte();
					metadataTransform.Clone(obj.Metadata);
					metadataTransform.MoveStartIndex(1);

					if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() =>
						{
							transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
							transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
						});
					}
					else if ((transformFlags & 0x01) != 0)
					{
						MainThreadManager.Run(() => { transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform); });
					}
					else if ((transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() => { transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform); });
					}
				}
			}

			MainThreadManager.Run(() =>
			{
				NetworkStart();
				networkObject.Networker.FlushCreateActions(networkObject);
			});
		}

		protected override void CompleteRegistration()
		{
			base.CompleteRegistration();
			networkObject.ReleaseCreateBuffer();
		}

		public override void Initialize(NetWorker networker, byte[] metadata = null)
		{
			Initialize(new R_PlayerNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new R_PlayerNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// bool b_adelante
		/// bool b_izquierda
		/// bool b_atras
		/// bool b_derecha
		/// bool b_saltar
		/// bool b_correr
		/// bool b_interactuar
		/// bool b_disparar
		/// bool b_recargar
		/// </summary>
		public abstract void R_Enviar_Estados(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// bool b_rotarPlayer
		/// bool b_rotarCamara
		/// float f_rotacionX
		/// float f_rotacionY
		/// </summary>
		public abstract void R_Enviar_Rotacion(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// float f_vidaPlayer
		/// </summary>
		public abstract void R_VidaPlayer(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_Disparar(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_Recargar(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_AnimDisparar(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_AnimRecargar(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_BalasCargador(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_RecibirDanyo(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_Mensaje_AbrirPuerta(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_Ronda(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}