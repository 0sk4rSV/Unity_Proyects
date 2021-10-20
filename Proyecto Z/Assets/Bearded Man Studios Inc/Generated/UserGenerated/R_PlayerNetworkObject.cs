using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.1,0.1,0.1]")]
	public partial class R_PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.1f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _rotationX;
		public event FieldEvent<Quaternion> rotationXChanged;
		public InterpolateQuaternion rotationXInterpolation = new InterpolateQuaternion() { LerpT = 0.1f, Enabled = true };
		public Quaternion rotationX
		{
			get { return _rotationX; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotationX == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotationX = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationXDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotationX(ulong timestep)
		{
			if (rotationXChanged != null) rotationXChanged(_rotationX, timestep);
			if (fieldAltered != null) fieldAltered("rotationX", _rotationX, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _rotationY;
		public event FieldEvent<Vector3> rotationYChanged;
		public InterpolateVector3 rotationYInterpolation = new InterpolateVector3() { LerpT = 0.1f, Enabled = true };
		public Vector3 rotationY
		{
			get { return _rotationY; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotationY == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_rotationY = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationYDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_rotationY(ulong timestep)
		{
			if (rotationYChanged != null) rotationYChanged(_rotationY, timestep);
			if (fieldAltered != null) fieldAltered("rotationY", _rotationY, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationXInterpolation.current = rotationXInterpolation.target;
			rotationYInterpolation.current = rotationYInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotationX);
			UnityObjectMapper.Instance.MapBytes(data, _rotationY);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotationX = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationXInterpolation.current = _rotationX;
			rotationXInterpolation.target = _rotationX;
			RunChange_rotationX(timestep);
			_rotationY = UnityObjectMapper.Instance.Map<Vector3>(payload);
			rotationYInterpolation.current = _rotationY;
			rotationYInterpolation.target = _rotationY;
			RunChange_rotationY(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotationX);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotationY);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationXInterpolation.Enabled)
				{
					rotationXInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationXInterpolation.Timestep = timestep;
				}
				else
				{
					_rotationX = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotationX(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (rotationYInterpolation.Enabled)
				{
					rotationYInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					rotationYInterpolation.Timestep = timestep;
				}
				else
				{
					_rotationY = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_rotationY(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationXInterpolation.Enabled && !rotationXInterpolation.current.UnityNear(rotationXInterpolation.target, 0.0015f))
			{
				_rotationX = (Quaternion)rotationXInterpolation.Interpolate();
				//RunChange_rotationX(rotationXInterpolation.Timestep);
			}
			if (rotationYInterpolation.Enabled && !rotationYInterpolation.current.UnityNear(rotationYInterpolation.target, 0.0015f))
			{
				_rotationY = (Vector3)rotationYInterpolation.Interpolate();
				//RunChange_rotationY(rotationYInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public R_PlayerNetworkObject() : base() { Initialize(); }
		public R_PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public R_PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
