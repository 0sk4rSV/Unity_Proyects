using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Reiniciar_Partida : MonoBehaviour
{
    Button boton_Reiniciar;
    Button boton_Menu;

    private void Start()
    {
        boton_Reiniciar = GameObject.Find("Boton_Reiniciar").GetComponent<Button>();
        boton_Menu = GameObject.Find("Boton_Menu").GetComponent<Button>();

        boton_Reiniciar.onClick.AddListener(OnClick_Reiniciar);
        boton_Menu.onClick.AddListener(OnClick_Menu);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OnClick_Reiniciar()
    {
        SceneManager.LoadScene("SampleScene");
        Destroy(GameObject.Find("Controlador_Puntos"));
    }
    void OnClick_Menu()
    {
        SceneManager.LoadScene("MultiplayerMenu");
    }

}
