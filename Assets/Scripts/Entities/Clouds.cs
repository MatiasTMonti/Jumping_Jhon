using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] private float movSpeed = 1.0f;

    private Rigidbody2D rigidBody = null;

    private Vector2 screenBounds;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void Update()
    {
        CloudsMove();
        FunctionAsd();
    }

    private void CloudsMove()
    {
        rigidBody.velocity = new Vector2(movSpeed, Time.deltaTime);
    }

    private void FunctionAsd()
    {
        if (transform.position.x > screenBounds.x + 2)
        {
            Destroy(this.gameObject);
        }
    }
}
