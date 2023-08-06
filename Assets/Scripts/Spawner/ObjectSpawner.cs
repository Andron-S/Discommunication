using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnObj;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private float _startTimeBtwSpanws;
    [SerializeField] private float _timeBtwSpanws;
    [SerializeField] private float _numberOfSpawnObjects;

    private int _rand;
    private int _randPosition;

    void Start()
    {
        _timeBtwSpanws = _startTimeBtwSpanws;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeBtwSpanws <= 0 && _numberOfSpawnObjects > 0)
        {
            _rand = Random.Range(0, _spawnObj.Length);
            _randPosition = Random.Range(0, _spawnPoints.Length);
            Instantiate(_spawnObj[_rand], _spawnPoints[_randPosition].transform.position, Quaternion.identity);

            _timeBtwSpanws = _startTimeBtwSpanws;
            _numberOfSpawnObjects -= 1;
        }
        else
        {
            _timeBtwSpanws -= Time.deltaTime;
        }
    }
}
