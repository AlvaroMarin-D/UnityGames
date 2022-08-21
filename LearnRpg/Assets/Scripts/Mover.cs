using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }



    protected virtual void UpdateMotor(Vector3 input)
    {

        // Rest MoveDelta
        moveDelta = new Vector3( input.x*xSpeed,input.y*ySpeed);

        //Swap Sprite Direction, wether you're going right or left (Changes orientation of the sprite)
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;

        }
        else if (moveDelta.x < 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);

        }

        //Add push vector if any
        moveDelta += pushDirection;

        //reduce push force based on recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Make Sure we Collide with wall
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Creatures", "Blocking"));

        if (hit.collider == null)
        {
            //Make the Sprite Move

            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Creatures", "Blocking"));

        if (hit.collider == null)
        {
            //Make the Sprite Move

            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
