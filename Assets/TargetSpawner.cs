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
        if (_currentTarget == null) {
            _currentTarget = Instantiate(_targetPrefab, pos, Quaternion.identity, gameObject.transform);
        } else {
            _currentTarget.transform.SetPositionAndRotation(pos, Quaternion.identity);
            _currentTarget.SetActive(true);
        }
    }

    private static Vector3 GetRandomPositionInViewport()
    {
        float x = Random.Range(0.05f, 0.95f);
        float y = Random.Range(0.05f, 0.95f);
        float z = Random.Range(3.5f, 7.5f);
        Vector3 pos = new Vector3(x, y, z);
        pos = Camera.main.ViewportToWorldPoint(pos);
        return pos;
    }

    private void FixedUpdate()
    {
        if (!_currentTarget.activeInHierarchy)
        {
            SpawnTarget();
        }
    }

    private void Start()
    {
        SpawnTarget();
    }
}
