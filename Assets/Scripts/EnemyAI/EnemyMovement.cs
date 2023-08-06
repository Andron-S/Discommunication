using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void SetNewDestination(Vector2 position)
    {
        Debug.Log($"{this}>>>moving to {position}");
        StopAllCoroutines();
        StartCoroutine(_MoveTo(position));
    }

    public void SetNewTargetDestination(Transform targetTransform)
    {
        StopAllCoroutines();
        StartCoroutine(_MoveToTransform(targetTransform));
    }

    public void StopMovment()
    {
        StopAllCoroutines();
    }

    private void _MoveToPos(Vector2 newPosition, float translationTime)
    {
        transform.position = newPosition;
    }

    private IEnumerator _MoveTo(Vector2 target)
    {
        while(Vector2.Distance(transform.position, target) >= 0.05)
        {
            var translateVector = target - (Vector2)transform.position;
            var dT = Time.deltaTime;
            var newPose = (Vector2)transform.position + translateVector.normalized * dT * _speed;
            _MoveToPos(newPose, dT);
            yield return true;
        }
    }

    private IEnumerator _MoveToTransform(Transform target)
    {
        while (Vector2.Distance(transform.position, target.position) >= 0.05)
        {
            var translateVector = target.position - transform.position;
            var dT = Time.deltaTime;
            var newPose = transform.position + translateVector * dT * _speed;
            _MoveToPos(newPose, dT);
            yield return true;
        }
    }
}
