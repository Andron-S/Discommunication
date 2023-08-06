using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMap : MonoBehaviour
{
    private List<Transform> _movePoints = new List<Transform>();

    private List<MapPointData> _mapPoints = new List<MapPointData>();

    private void Awake()
    {
        InitializeMapPoints();
    }

    public Vector2 GetNewRandomFreeMovePointFor(EnemyAI enemy)
    {
        var busyPoints = _mapPoints.FindAll(x => x.IsBuisy);
        foreach(var bPoint in busyPoints)
        {
            bPoint.ClearPoint(enemy);
        }

        var freePoints = _mapPoints.FindAll(x => !x.IsBuisy);

        var point = freePoints[Random.Range(0, freePoints.Count)];

        return point.GetMovePositionFor(enemy);
    }

    private void InitializeMapPoints()
    {
        _movePoints = new List<Transform>( GetComponentsInChildren<Transform>());
        _mapPoints = new List<MapPointData>();

        foreach (var tr in _movePoints)
        {
            _mapPoints.Add(new MapPointData(tr));
        }
    }
}

public class MapPointData
{
    public EnemyAI _movingEnemy = null;
    public bool IsBuisy { get => _movingEnemy != null; }

    private Transform _transform = null;


    public MapPointData(Transform transform)
    {
        _transform = transform;
    }


    public void ClearPoint(EnemyAI enemy)
    {
        if(_movingEnemy == enemy)
        {
            _movingEnemy = null;
        }
    }

    public Vector2 GetMovePositionFor(EnemyAI enemy)
    {
        _movingEnemy = enemy;
        return _transform.position;
    }
}
