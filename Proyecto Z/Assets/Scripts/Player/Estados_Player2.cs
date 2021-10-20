using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estados_Player2 : MonoBehaviour //Si se esta ejecutando en el creador del objeto, se encargara de indicar los estados del objeto.
{
    public GameObject go_controles_android;
    public bool b_android = false;
    public bool B_android { get => b_android; }

    public FixedJoystick fj_movimiento;
    public FixedJoystick fj_camara;

    Button boton_saltar;
    Button boton_recargar;
    Button boton_disparar;
    Button boton_interactuar;
    Button boton_bloquearCamara;

    float f_timer = 0.0f;
    bool b_cambioCamara = false;

    public bool b_adelante = false;
    public bool B_adelante { get => b_adelante; set => b_adelante = value; }

    public bool b_izquierda = false;
    public bool B_izquierda { get => b_izquierda; set => b_izquierda = value; }

    public bool b_derecha = false;
    public bool B_derecha { get => b_derecha; set => b_derecha = value; }

    public bool b_atras = false;
    public bool B_atras { get => b_atras; set => b_atras = value; }

    public bool b_saltar = false;
    public bool B_saltar { get => b_saltar; set => b_saltar = value; }

    public bool b_correr = false;
    public bool B_correr { get => b_correr; set => b_correr = value; }

    public bool b_interactuar = false;
    public bool B_interactuar { get => b_interactuar; set => b_interactuar = value; }

    public bool b_disparar = false;
    public bool B_disparar { get => b_disparar; set => b_disparar = value; }

    public bool b_recargar = false;
    public bool B_recargar { get => b_recargar; set => b_recargar = value; }

    public bool b_rotarPlayer = false;
    public bool B_rotarPlayer { get => b_rotarPlayer; set => b_rotarPlayer = value; }

    public bool b_rotarCamara = false;
    public bool B_rotarCamara { get => b_rotarCamara; set => b_rotarCamara = value; }

    public float f_mouse_x = 0f;
    public float F_mouse_x { get => f_mouse_x; set => f_mouse_x = value; }

    public float f_mouse_y = 0f;
    public float F_mouse_y { get => f_mouse_y; set => f_mouse_y = value; }

    float pointer_x;
    float pointer_y;

    void Start()
    {
        go_controles_android = transform.Find("Canvas_Joysticks").gameObject;

        boton_saltar = GameObject.Find("Boton_Saltar").GetComponent<Button>();
        boton_recargar = GameObject.Find("Boton_Recargar").GetComponent<Button>();
        boton_disparar = GameObject.Find("Boton_Disparar").GetComponent<Button>();
        boton_interactuar = GameObject.Find("Boton_Interactuar").GetComponent<Button>();
        boton_bloquearCamara = GameObject.Find("Boton_BloquearCamara").GetComponent<Button>();

        boton_saltar.onClick.AddListener(OnClick_Saltar);
        boton_recargar.onClick.AddListener(OnClick_Recargar);
        boton_disparar.onClick.AddListener(OnClick_Disparar);
        boton_bloquearCamara.onClick.AddListener(OnClick_BloquearCamara);
        boton_interactuar.onClick.AddListener(OnClick_Interactuar);

        go_controles_android.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        f_timer += Time.deltaTime;

        if (!GetComponent<Player_Gestor2>().b_creador)
            return;

        if (!b_android && !Input.touchSupported)
        {
            go_controles_android.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (Input.GetKey(KeyCode.D))
                b_derecha = true;
            else
                b_derecha = false;

            if (Input.GetKey(KeyCode.A))
                b_izquierda = true;
            else
                b_izquierda = false;

            if (Input.GetKey(KeyCode.W))
                b_adelante = true;
            else
                b_adelante = false;

            if (Input.GetKey(KeyCode.S))
                b_atras = true;
            else
                b_atras = false;

            if (Input.GetKey(KeyCode.LeftShift))
                b_correr = true;
            else
                b_correr = false;

            if (Input.GetKey(KeyCode.E))
                b_interactuar = true;
            else
                b_interactuar = false;

            if (Input.GetKey(KeyCode.Space))
                b_saltar = true;
            else
                b_saltar = false;

            if (Input.GetMouseButtonDown(0))
                b_disparar = true;
            else
                b_disparar = false;

            if (Input.GetKey(KeyCode.R))
                b_recargar = true;
            else
                b_recargar = false;

            if (Input.GetKeyDown(KeyCode.Mouse2))
                b_cambioCamara = !b_cambioCamara;


            b_rotarPlayer = true;
            b_rotarCamara = true;

            if (b_cambioCamara == false)
            {
                GetComponent<Player_Gestor2>().R_Aviso_Enviar_Rotacion(b_rotarPlayer, b_rotarCamara, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
            else
            {
                GetComponent<Player_Gestor2>().R_Aviso_Enviar_Rotacion(b_rotarPlayer, b_rotarCamara, Input.GetAxis("Mouse X"), 0.0f);
            }

            GetComponent<Player_Gestor2>().R_Aviso_Enviar_Estados(b_adelante, b_izquierda, b_atras, b_derecha, b_saltar, b_correr, b_interactuar, b_disparar, b_recargar);
        }
        else
        {
            go_controles_android.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (fj_movimiento.Horizontal > 0.1)
                b_derecha = true;
            else
                b_derecha = false;

            if (fj_movimiento.Horizontal < -0.1)
                b_izquierda = true;
            else
                b_izquierda = false;

            if (fj_movimiento.Vertical > 0.1)
                b_adelante = true;
            else
                b_adelante = false;

            if (fj_movimiento.Vertical < -0.1)
                b_atras = true;
            else
                b_atras = false;
            if (fj_movimiento.Vertical > 0.8)
                b_correr = true;
            else
                b_correr = false;

            if (f_timer >= 0.05f)
            {
                b_saltar = false;
                b_recargar = false;
                b_disparar = false;
                b_interactuar = false;
            }

            if (fj_camara.Horizontal > 0.01 || fj_camara.Horizontal < -0.01)
            {
                b_rotarPlayer = true;

            }
            else
                b_rotarPlayer = false;

            if (fj_camara.Vertical > 0.01 || fj_camara.Vertical < -0.01)
            {
                b_rotarCamara = true;
            }
            else
                b_rotarCamara = false;

            if (Input.touchCount > 0)
            {
                pointer_x = Input.touches[0].deltaPosition.x;
                pointer_y = Input.touches[0].deltaPosition.y;
            }

            if (b_cambioCamara == false)
            {
                GetComponent<Player_Gestor2>().R_Aviso_Enviar_Rotacion(b_rotarPlayer, b_rotarCamara, pointer_x / 8, pointer_y / 8);
            }
            else
            {
                GetComponent<Player_Gestor2>().R_Aviso_Enviar_Rotacion(b_rotarPlayer, b_rotarCamara, pointer_x / 8, 0.0f);
            }
            GetComponent<Player_Gestor2>().R_Aviso_Enviar_Estados(b_adelante, b_izquierda, b_atras, b_derecha, b_saltar, b_correr, b_interactuar, b_disparar, b_recargar);
        }

    }

    void OnClick_Saltar()
    {
        b_saltar = true;
        f_timer = 0.0f;
    }
    void OnClick_Recargar()
    {
        b_recargar = true;
        f_timer = 0.0f;
    }
    void OnClick_Disparar()
    {
        b_disparar = true;
        f_timer = 0.0f;
    }
    void OnClick_Interactuar()
    {
        b_interactuar = true;
        f_timer = 0.0f;
    }
    void OnClick_BloquearCamara()
    {
        b_cambioCamara = !b_cambioCamara;
        f_timer = 0.0f;
    }

}