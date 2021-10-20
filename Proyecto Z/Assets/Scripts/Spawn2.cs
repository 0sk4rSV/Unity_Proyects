using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn2 : MonoBehaviour
{
    public GameObject go_zombie;
    Control_Rondas controlRondas;
    float f_timer = -8.5f;
    float f_spawnDelay = 2f;
    public int i_zombiesPorInstanciar;
    public int i_contZombiesInstanciados;

    void Start()
    {
        controlRondas = GameObject.Find("Controlador_Rondas").GetComponent<Control_Rondas>();
    }

    void Update()
    {
        f_timer += Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (f_timer >= f_spawnDelay && controlRondas.I_zombiesPorInstanciar != 0 && i_contZombiesInstanciados < 24)
            {
                SpawnearZombie();
            }
        }
    }

    public void SpawnearZombie()
    {
        setTimer(0.0f);

        NetworkManager.Instance.InstantiateR_Zombie(0, gameObject.transform.position, gameObject.transform.rotation);

        controlRondas.I_zombiesPorInstanciar --;
        i_contZombiesInstanciados++;
    }

    public void DisminuirZombiesInstanciados()
    {
        i_contZombiesInstanciados--;
    }

    public void setSpawnDelay()
    {
        f_spawnDelay *= 0.95f;
    }

    public void setTimer(float f_time)
    {
        f_timer = f_time;
    }

}
