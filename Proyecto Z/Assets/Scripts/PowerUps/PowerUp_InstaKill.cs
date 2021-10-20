using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PowerUp_InstaKill : MonoBehaviour
{
    public AudioClip ac_sonidoInstaKill;
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
        Control_Arma controlArma = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Control_Arma>();
        GetComponent<AudioSource>().PlayOneShot(ac_sonidoInstaKill);

        controlArma.F_tiempoInstaKill = 30.0f;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        yield return new WaitForSeconds(4f);

        GetComponent<R_PowerUp_Gestor>().R_Aviso_Destruir_PowerUp();
    }

    public void DesaparecePowerUp()
    {
        if (b_desaparece)
            GetComponent<R_PowerUp_Gestor>().R_Aviso_Destruir_PowerUp();
    }

}
