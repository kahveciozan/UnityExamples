using Assets._MyExampleProjects.Coroutines.Events;
using StarterAssets;
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
    private UnityEvent onEnemyCloseToPlayer;                            // An event always starts with the letter "O"

    [SerializeField]
    private UnityEventForEnemyCloseBy onEnemyCloseToPlayerProvideInfo;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(CheckForEnemies());

        onEnemyCloseToPlayer.AddListener(() => PlayerIsCloseToEnemy());

        onEnemyCloseToPlayerProvideInfo.AddListener((enemyName, distance) => PlayerIsCloseToEnemy(enemyName,distance));
    }

    void PlayerIsCloseToEnemy()
    {
        Logger.Instance.LogInfo("Enemy is close to the player");
    }

    void PlayerIsCloseToEnemy(string enemyName, float distanece)
    {
        Logger.Instance.LogInfo($"Enemy: {enemyName} " + $"is closed by {distanece}");

        GameObject enemy = GameObject.Find(enemyName);
        Renderer renderer = enemy.GetComponent<Renderer>();
        renderer.material.color = Color.yellow;


        VariablesAndInspector playerFeatures = FindAnyObjectByType<VariablesAndInspector>();        // Put a debug point here and Attach to Unity and then go Gameplay screen and Play.
        Debug.Log($"Player Features was loaded {playerFeatures.PlayerName}");

        ThirdPersonController controller = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        //controller.MoveSpeed = .5f;
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
                    onEnemyCloseToPlayer?.Invoke();                                                       // "?" Nullable value type

                    onEnemyCloseToPlayerProvideInfo?.Invoke(e.name, distance);
                    //Logger.Instance.LogInfo($"Enemy{e.name} is closed by {distance} ");
                    //StopCoroutine(coroutine);
                }
            }

            yield return new WaitForSeconds(frequency);


        }
    }


}
