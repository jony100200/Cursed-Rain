using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] protected GameObject _knifeGameObject;
    [Header("Spawn Location")]
    [SerializeField] protected float _min_X = -2.8f;
    [SerializeField] protected float _max_X = 2.8f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,1f));

        GameObject k = Instantiate(_knifeGameObject) as GameObject;

        float x = Random.Range(_min_X, _max_X);

        k.transform.position = new Vector2(x,transform.position.y);

        StartCoroutine(StartSpawning());
    }
}
