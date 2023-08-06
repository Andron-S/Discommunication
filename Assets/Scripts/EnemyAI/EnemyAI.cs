using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyAttackType
{
    Melee,
    Range
}

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Enemy _controllingEnemy;
    [SerializeField] private AIMap _connectedMap = null;
    [SerializeField] private EnemyAttackType _enemyAttackType = EnemyAttackType.Melee;
    [SerializeField] private float _playerDetectionRange = 10f;
    [SerializeField] private float _enemyAttackDistance = 8f;
    [SerializeField] private float _constantAttackDelay = 0.3f;
    [SerializeField] private Player _player = null;

    private EnemyMovement _movmentController;

    //private static Player _player = null;
    private static List<EnemyAI> _chasingEnemys = new List<EnemyAI>();
    private static int _maxChasingEnemy = 3;
    private static float _behaviourTickTime = 0.2f;
    private bool _playerDetected = false;
    private bool _isChasing = false;

    private int _movementBehaviourCounter = 0;


    private void Start()
    {
        
        if(_connectedMap == null)
        {
            _connectedMap = FindAnyObjectByType<AIMap>();
        }
        _movmentController = _controllingEnemy.GetComponent<EnemyMovement>();
        RestartAlgorythm();
    }


    protected void RestartAlgorythm()
    {
        StopAllCoroutines();
        StartCoroutine(_AIAlgorythm());
    }

    protected virtual void _UpdatePlayerDetection()
    {
        if(GetDistanceToPlayer(this) <= _playerDetectionRange)
        {
            _playerDetected = true;
        }
        else
        {
            _playerDetected = false;
            _isChasing = false;
        }
    }


    protected virtual void _UpdatePlayerMovmentBehaviour()
    {
        if (_playerDetected)
        {
            if (!_isChasing && _TryChasePlayer())
            {
                return;
            }
            else if (_isChasing)
            {
                return;
            }
        }


        if(_movementBehaviourCounter == 0)
        {
            _MoveTo(_connectedMap.GetNewRandomFreeMovePointFor(this));
        }

        _UpdateMovmentCounter();
    }


    protected void _StartConstantAttack()
    {
        StartCoroutine(ConstantAttack());
    }

    protected void _StopConstantAttack()
    {
        StopCoroutine(ConstantAttack());
    }

    protected bool _TryAttack()
    {
        if(GetDistanceToPlayer(this) < _enemyAttackDistance)
        {
            _controllingEnemy.AttackWeapon();
            return true;
        }
        else
        {
            return false;
        }
    }


    protected void _MoveTo(Vector2 target)
    {
        _movmentController.SetNewDestination(target);
        _chasingEnemys.Remove(this);
    }


    protected bool _TryChasePlayer()
    {
        if(_chasingEnemys.Contains(this) || _chasingEnemys.Count < _maxChasingEnemy)
        {
            _movmentController.SetNewTargetDestination(_player.transform);
            _chasingEnemys.Add(this);
            _isChasing = true;
            return true;
        }
        _isChasing = false;
        return this;
    }


    protected IEnumerator _AIAlgorythm()
    {
        while (true)
        {
            _UpdatePlayerDetection();
            _UpdatePlayerMovmentBehaviour();
            yield return new WaitForSeconds(_behaviourTickTime);
        }
    }


    public IEnumerator ConstantAttack()
    {
        while (true)
        {
            _TryAttack();
            yield return new WaitForSeconds(_constantAttackDelay);
        }
    }


    public static float GetDistanceToPlayer(EnemyAI enemy)
    {
        return Vector2.Distance(enemy.transform.position, enemy._player.transform.position);
    }


    private void _UpdateMovmentCounter()
    {
        if(_movementBehaviourCounter <= 0)
        {
            _movementBehaviourCounter = Random.Range(20, 41);
        }
        else
        {
            _movementBehaviourCounter--;
        }
    }
}


