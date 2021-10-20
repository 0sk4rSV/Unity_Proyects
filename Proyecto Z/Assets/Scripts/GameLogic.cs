using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Instance.InstantiateR_Player(0, new Vector3(28.56f, 1.0f, 87.1f), new Quaternion(0, -0.97f, 0, 0.22f));
    }

}

