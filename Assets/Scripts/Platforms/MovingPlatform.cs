using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private int leftRight = 1;

    [SerializeField] private bool switchUp = false;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float maxX = 1.0f;
    [SerializeField] private float speedMin = 1.0f;
    [SerializeField] private float speedMax = 1.0f;

    [SerializeField] private GameObject platform = null;

    private void OnEnable()
    {
        leftRight = Random.Range(0, 2);

        if (leftRight == 1)
        {
            switchUp = true;
        }
        else
        {
            switchUp = false;
        }
        speed = Random.Range(speedMin, speedMax);
    }

    private void Update()
    {
        MovementFunction();
    }

    private void MovementFunction()
    {
        if (platform.transform.position.x >= maxX && switchUp == true)
        {
            switchUp = false;
            leftRight = 0;
        }
        else if (platform.transform.position.x <= -maxX && switchUp == false)
        {
            switchUp = true;
            leftRight = 1;
        }
        else
        {
            if (leftRight == 0)
            {
                platform.transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime);
            }
            else if (leftRight == 1)
            {
                platform.transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
            }
        }
    }
}
