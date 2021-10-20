using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_PowerUps : MonoBehaviour
{
    public int i_num1 = 0;
    public int I_num1 { get => i_num1; set => i_num1 = value; }
   public  int i_num2 = 0;
    public int I_num2 { get => i_num2; set => i_num2 = value; }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void RandomizarNumeros(int i_numMax)
    {
        i_num1 = Random.Range(1, i_numMax + 1);
        i_num2 = Random.Range(1, i_numMax + 1);
        if (i_num1 == i_num2)
        i_num2 = Random.Range(1, i_numMax + 1);

    }
}
