using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomShifter : MonoBehaviour
{
    [SerializeField] private float _minShiftDistance = 0.3f;
    [SerializeField] private float _maxShiftDistance = 0.5f;
    [Space]
    [SerializeField] private float _minShiftTime = 0.7f;
    [SerializeField] private float _maxShiftTime = 1.2f;

    public void Shift()
    {
        var shiftVector = GetRandomVector(_minShiftDistance, _maxShiftDistance);
        var shiftTime = Random.Range(_minShiftTime, _maxShiftTime);
        transform.DOBlendableLocalMoveBy(shiftVector, shiftTime);
    }

    public static Vector2 GetRandomVector(float minMagnitude, float maxMagnitude)
    {
        var angle = Random.Range(0, 2 * Mathf.PI);
        var radius = Random.Range(minMagnitude, maxMagnitude);
        var dirVector = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        return dirVector * radius;
        
    }
}
