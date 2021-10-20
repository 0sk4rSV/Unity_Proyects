using UnityEngine;

public class Rotar : MonoBehaviour
{
    public float f_velocidadGiro = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, f_velocidadGiro * Time.deltaTime);
    }

}
