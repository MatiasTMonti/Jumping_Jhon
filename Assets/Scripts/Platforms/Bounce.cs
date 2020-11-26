using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float JumpForce = 1.0f;
    [SerializeField] private AudioClip bounceSfx = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * JumpForce);

            if (collision.gameObject.CompareTag("Player"))
            {
                AudioController.instance.PlaySound(bounceSfx);
            }
        }
    } 
}
