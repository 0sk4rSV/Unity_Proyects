using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Estados_Player2))]

public class Movimiento_Personaje2 : MonoBehaviour
{
    public GameObject go_manos;

    Estados_Player2 estadosPlayer;

    CharacterController cc_Player;

    float f_velocidad = 4.3f;
    float f_salto = 4f;
    public float f_sensibilidad = 135f;
    float f_rotationY = 0f;
    Vector3 v3_l_rotacion = Vector3.zero;
    Vector3 v3_g_gravedad = new Vector3(0f, -9.8f, 0f);
    Vector3 v3_l_velocidad = Vector3.zero;

    void Start()
    {
        go_manos = transform.Find("Manos").gameObject;
        estadosPlayer = GetComponent<Estados_Player2>();
        cc_Player = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        if (!GetComponent<Player_Gestor2>().networkObject.IsOwner)
            return;

        if (cc_Player.isGrounded)
        {
            v3_l_velocidad = Vector3.zero;

            if (estadosPlayer.B_derecha)
                v3_l_velocidad += Vector3.right * f_velocidad;
            if (estadosPlayer.B_izquierda)
                v3_l_velocidad += Vector3.left * f_velocidad;
            if (estadosPlayer.B_adelante)
                v3_l_velocidad += Vector3.forward * f_velocidad;
            if (estadosPlayer.B_atras)
                v3_l_velocidad += Vector3.back * f_velocidad;

            if (estadosPlayer.B_saltar)
                v3_l_velocidad += Vector3.up * f_salto;
            if (estadosPlayer.B_adelante && estadosPlayer.B_correr)
                v3_l_velocidad += Vector3.forward * (f_velocidad + 1);
        }

        //Rotacion personaje
        v3_l_velocidad += v3_g_gravedad * Time.deltaTime;
        Vector3 v3_l_distancia = v3_l_velocidad * Time.deltaTime;
        cc_Player.Move(transform.TransformVector(v3_l_distancia));

        f_rotationY = estadosPlayer.F_mouse_x * f_sensibilidad * Time.deltaTime;

        if (estadosPlayer.b_rotarPlayer)
        {
            transform.Rotate(0, f_rotationY, 0);
        }

        //Rotacion manos
        v3_l_rotacion.x -= estadosPlayer.F_mouse_y * f_sensibilidad * Time.deltaTime;
        v3_l_rotacion.x = Mathf.Clamp(v3_l_rotacion.x, -90f, 90f);

        if (estadosPlayer.b_rotarCamara)
        {
            MoverManos(v3_l_rotacion);
        }
    }

    public void MoverManos(Vector3 v3_l_rotacion)
    {
        go_manos.transform.localEulerAngles = v3_l_rotacion;
    }

}