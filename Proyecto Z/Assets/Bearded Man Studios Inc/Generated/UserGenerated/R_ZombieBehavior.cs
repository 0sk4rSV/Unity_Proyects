using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"Vector3\", \"Quaternion\"][][][][\"int\"][]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"position\", \"rotation\"][][][][\"i_daño\"][]]")]
	public abstract partial class R_ZombieBehavior : NetworkBehavior
	{
		public const byte RPC_POSICION__INICIAL = 0 + 5;
		public const byte RPC_ATACAR = 1 + 5;
		public const byte RPC_R__MUERTE__ZOMBIE = 2 + 5;
		public const byte RPC_CANCELAR_ATAQUE = 3 + 5;
		public const byte RPC_RECIBIR_DAÑO = 4 + 5;
		public const byte RPC_R__ANIM__MUERTE = 5 + 5;
		
		public R_ZombieNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (R_ZombieNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("Posicion_Inicial", Posicion_Inicial, typeof(Vector3), typeof(Quaternion));
			networkObject.RegisterRpc("Atacar", Atacar);
			networkObject.RegisterRpc("R_Muerte_Zombie", R_Muerte_Zombie);
			networkObject.RegisterRpc("CancelarAtaque", CancelarAtaque);
			networkObject.RegisterRpc("RecibirDaño", RecibirDaño, typeof(int));
			networkObject.RegisterRpc("R_Anim_Muerte", R_Anim_Muerte);

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
			Initialize(new R_ZombieNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new R_ZombieNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// Vector3 position
		/// Quaternion rotation
		/// </summary>
		public abstract void Posicion_Inicial(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void Atacar(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_Muerte_Zombie(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void CancelarAtaque(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void RecibirDaño(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void R_Anim_Muerte(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}