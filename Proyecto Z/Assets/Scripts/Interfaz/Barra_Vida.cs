using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class Barra_Vida : MonoBehaviour
{
    Slider slider_vida;

    void Start()
    {
        slider_vida = GameObject.Find("Barra_Vida").GetComponent<Slider>();
        slider_vida.maxValue = 200;
    }

    private void Update()
    {
        GameObject go_player = GameObject.FindGameObjectWithTag("Player");
        slider_vida.value = go_player.GetComponent<Vida_Player>().f_vidaMaxima;
    }

    public void ActualizarBarraVida(float f_vida)
    {
        slider_vida.value = (int)f_vida;
    }

}
