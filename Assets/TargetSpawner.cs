using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetPrefab;
    private GameObject _currentTarget;

    public void SpawnTarget()
    {
        Vector3 pos = GetRandomPositionInViewport();
        _currentTarget = Instantiate(_targetPrefab, pos, Quaternion.identity, gameObject.transform);
    }

    private static Vector3 GetRandomPositionInViewport()
    {
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        Vector3 pos = new Vector3(x, y, 10.0f);
        pos = Camera.main.ViewportToWorldPoint(pos);
        return pos;
    }

    private void FixedUpdate()
    {
        if (_currentTarget == null)
        {
            SpawnTarget();
        }
    }

    private void Start()
    {
        SpawnTarget();
    }
}
