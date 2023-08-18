using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCoroutines : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] enemyCubes;

    [SerializeField]
    private float frequency = 0.5f;

    [SerializeField]
    private float minDistanceFromEnemy = 1.0f;

    private Coroutine coroutine;

    [SerializeField]
    private UnityEvent onEnemyCloseToPlayer;    // An event always starts with the lette "O"


    // Start is called before the first frame update
    void Start()
    {
         coroutine = StartCoroutine(CheckForEnemies());
    }

    IEnumerator CheckForEnemies()
    {
        while (true)
        {
            Debug.Log("Checking For Enemies..."); Logger.Instance.LogInfo("Checking For Enemies...");

            foreach (var e in enemyCubes)
            {
                var enemyPosition = e.transform.position;
                var playerPosition = player.transform.position;
                var distance = Vector3.Distance(playerPosition, enemyPosition);

                if (distance < minDistanceFromEnemy)
                {
                    onEnemyCloseToPlayer?.Invoke();                                                       // Nullable value type

                    Logger.Instance.LogInfo($"Enemy{e.name} is closed by {distance} ");

                    StopCoroutine(coroutine);
                }
            }

            yield return new WaitForSeconds(frequency);


        }
    }


}
