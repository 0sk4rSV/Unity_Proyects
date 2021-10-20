using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cerrar_Juego : MonoBehaviour
{
    Button boton_Salir;
    // Start is called before the first frame update
    void Start()
    {
        boton_Salir = GameObject.Find("Boton_Salir").GetComponent<Button>();

        boton_Salir.onClick.AddListener(OnClick_Salir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick_Salir()
    {
        Application.Quit();
    }
}
