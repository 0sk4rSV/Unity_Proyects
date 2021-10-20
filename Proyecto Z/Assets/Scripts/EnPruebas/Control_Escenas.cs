using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum GameState {Desactivado, Activado, Finalizado};

public class Control_Escenas : MonoBehaviour
{
    Vida_Player vidaPlayer;
    GameState gameState = GameState.Activado;

    void Start()
    {
        vidaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida_Player>();
    }

    // Update is called once per frame
    void Update()
    {

            gameState = GameState.Finalizado;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AudioListener>().enabled = false;
     
            if (gameState == GameState.Finalizado && (Input.GetKey(KeyCode.F)))
            {
                ReiniciarJuego();
            
        }
        }
        

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
