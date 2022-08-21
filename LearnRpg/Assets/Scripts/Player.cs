using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private void FixedUpdate()
    {
        //Input Variables. you can check the inputs under 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
}
