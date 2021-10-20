using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PowerUp_Nuke : MonoBehaviour
{
    GameObject[] array_go_zombies;
    public AudioClip ac_sonidoNuke;
    public bool b_desaparece = true;

    void Start()
    {
        Invoke("DesaparecePowerUp", 30.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Coger());
        }
    }

    public IEnumerator Coger()
    {
        Spawn2 spawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Spawn2>();

        GetComponent<AudioSource>().PlayOneShot(ac_sonidoNuke);

        array_go_zombies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject go_zombie in array_go_zombies)
        {
            go_zombie.GetComponent<Control_Zombie>().F_vidaZombie = 0.0f;
        }

        GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().sumaPuntos(400);
        spawn.setTimer(-8.0f);

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(3f);

        GetComponent<R_PowerUp_Gestor>().R_Aviso_Destruir_PowerUp();
    }

    public void DesaparecePowerUp()
    {
        if (b_desaparece)
            GetComponent<R_PowerUp_Gestor>().R_Aviso_Destruir_PowerUp();
    }

}
