using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsDeploy : MonoBehaviour
{
    [SerializeField] private GameObject cloudsPrefab = null;
    [SerializeField] private float respawnTime = 1.0f;

    [SerializeField] private Transform holder = null; 

    private Vector2 ScreenBounds;

    private void Start()
    {
        ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(CloudsWave());
    }

    private void SpawnClouds()
    {

        GameObject clouds = Instantiate(cloudsPrefab);
        clouds.transform.SetParent(holder);
        clouds.transform.localPosition = new Vector2(ScreenBounds.x * -2, holder.transform.localPosition.y + Random.Range(-ScreenBounds.y, ScreenBounds.y));
    }

    private IEnumerator CloudsWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnClouds();
        }
    }
}
