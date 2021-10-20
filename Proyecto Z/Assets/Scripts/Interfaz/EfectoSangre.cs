using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class EfectoSangre : MonoBehaviour
{
    Image img_efectoSangre;
    float f_transparencia = 0f;
    Vida_Player vidaPlayer;

    void Start()
    {
        img_efectoSangre = GetComponent<Image>();
        vidaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida_Player>();
    }
    void Update()
    {
        if (vidaPlayer.f_vidaPlayer == 200)
        {
            f_transparencia = 0f;
        }
        else
        {
            if (vidaPlayer.f_vidaPlayer >= 150)
            {
                f_transparencia = 0.25f;
            }
            else
            {
                if (vidaPlayer.f_vidaPlayer >= 100)
                {
                    f_transparencia = 0.50f;
                }
                else
                {
                    if (vidaPlayer.f_vidaPlayer >= 50)
                    {
                        f_transparencia = 0.75f;
                    }
                    else
                    {
                        if (vidaPlayer.f_vidaPlayer >= 0)
                        {
                            f_transparencia = 1f;
                        }
                    }
                }
            }
        }
        f_transparencia = Mathf.Clamp(f_transparencia, 0f, 1f);
        CambiarColor();
    }

    private void CambiarColor()
    {
        Color color = new Color(img_efectoSangre.color.r, img_efectoSangre.color.g, img_efectoSangre.color.b, f_transparencia);
        img_efectoSangre.color = color;
    }

}
