using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHookAttached : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            PlayerHook.isHookMoving = false;
            PlayerHook.isHookStuck = true;

            Rigidbody2D hookRigidbody = GetComponent<Rigidbody2D>();
            hookRigidbody.velocity = Vector2.zero;

            hookRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        else if(collision.gameObject.tag == "UnHookable")
        {
            PlayerHook.isBouncing = true;

            Vector2 bounceDirection = Vector2.Reflect(GetComponent<Rigidbody2D>().velocity.normalized, collision.contacts[0].normal);

            float bounceSpeed = 2f;
            GetComponent<Rigidbody2D>().velocity = bounceDirection * bounceSpeed;
        }
    }
}
