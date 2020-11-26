using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 30f;

    private float secondsToDestroy = 5f;

    [SerializeField] private Transform levelPart_Start = null;
    [SerializeField] private List <Transform> levelPartList = null;
    [SerializeField] private Player player = null;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        int startingSpawnLevelParts = 5;

        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    void Start()
    {
        //StartCoroutine(DestroySelf());
    }

    private void Update()
    {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    /*IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }*/
}
