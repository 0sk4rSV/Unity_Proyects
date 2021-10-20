using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PowerUp_MaxAmmo : MonoBehaviour
{
    public AudioClip ac_sonidoMaxAmmo;
    public bool b_desaparece = true;

    void Start()
    {
        Invoke("DesaparecePowerUp", 30.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           Coger();
        }
    }

    public void Coger()
    {
        GetComponent<AudioSource>().PlayOneShot(ac_sonidoMaxAmmo);

        GameObject.FindGameObjectWithTag("Arma").GetComponent<Control_Arma>().setMunicionMaxima();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        Invoke("DestruyePowerUp", 3);
    }

    public void DesaparecePowerUp()
    {
        if (b_desaparece)
            GetComponent<R_PowerUp_Gestor>().R_Aviso_Destruir_PowerUp();
    }

    public void DestruyePowerUp()
    {
        GetComponent<R_PowerUp_Gestor>().R_Aviso_Destruir_PowerUp();
    }

}
