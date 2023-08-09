using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RandomPrefabSwapnwer : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();

    private GameObject randomGameObject;

    private GameObject E1,E2;

    [SerializeField] GameObject gameplay;
    [SerializeField] GameObject selectPrefabParent;
    // Start is called before the first frame update
    void Start()
    {
        //RandomSpawner();
        //InvokeRepeating("RandomSpawner",3f,3f);
    }

    private void RandomSpawner()
    {
        if(randomGameObject != null)
        {
            Destroy(randomGameObject);
        }

        int prefabIndex = Random.Range(0, prefabList.Count);
        randomGameObject = Instantiate(prefabList[prefabIndex]);
    }

    public void ManuelSpawner(GameObject prefab)
    {
        E1 = Instantiate(prefab);

        _camera.clearFlags = CameraClearFlags.Skybox;
        _camera.cullingMask = -1;                               // -1 means "Everything"

        gameplay.SetActive(true);
        selectPrefabParent.SetActive(false);
    }

    public void BiggerObjectSize(GameObject g)
    {
        Debug.Log("Enter");
        g.transform.localScale *= 1.5f;
    }

    public void SmalleerObjectSize(GameObject g)
    {
        Debug.Log("Exit");
        g.transform.localScale /= 1.5f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


}
