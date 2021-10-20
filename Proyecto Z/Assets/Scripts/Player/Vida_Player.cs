using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class Vida_Player : MonoBehaviour
{
    public float f_vidaPlayer = 200f;
    public float f_vidaMaxima = 200f;
    float f_timer = 0f;
    float f_incrementoPorSegundo = 35f;
    Text txt_vida;
    bool b_golpeado = true;
    Barra_Vida barraVida;

    void Start()
    {
        txt_vida = GameObject.Find("Texto_Vida").GetComponent<Text>();
        barraVida = GameObject.Find("Barra_Vida").GetComponent<Barra_Vida>();
    }

    void Update()
    {
        if (!GetComponent<Player_Gestor2>().b_creador)
            return;

        if (f_timer >= 4.5f)
        {
            b_golpeado = false;
        }

        if (f_vidaPlayer < f_vidaMaxima && b_golpeado == false)
        {

            f_vidaPlayer += f_incrementoPorSegundo * Time.deltaTime;
            if (f_vidaPlayer > f_vidaMaxima)
            {
                f_vidaPlayer = 200;
            }
            if (f_vidaPlayer < 0)
            {
                f_vidaPlayer = 0;
            }
        }

        barraVida.ActualizarBarraVida(f_vidaPlayer);
        setTextoVida(f_vidaPlayer);

        if (f_vidaPlayer <= 0)
        {
            SceneManager.LoadScene("Escena_FinPartida"); //Cambiar de escena al morir.
            GameObject.Find("Controlador_Puntos").GetComponent<Puntos>().getMarcadores();
        }

        f_timer += Time.deltaTime;
    }

    public void RecibirDaño(int i_daño)
    {
        if (!GetComponent<Player_Gestor2>().b_creador)
            return;

        f_timer = 0;
        f_vidaPlayer -= i_daño;
        if (f_vidaPlayer <= 0)
        {
            f_vidaPlayer = 0;
        }
        b_golpeado = true;
    }

    public void setTextoVida(float f_vidaPlayer)
    {
        if (!GetComponent<Player_Gestor2>().b_creador)
            return;

        txt_vida.text = ((int)f_vidaPlayer).ToString();
    }

}