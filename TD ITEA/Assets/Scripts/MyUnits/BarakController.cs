using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarakController : MonoBehaviour
{
    [SerializeField] private GameObject _soldierprefab;
    [SerializeField] private Transform _spawnSoldier;
    [SerializeField] private List<GameObject> _soliderObj = new List<GameObject>();
    private bool _isSpawn = false;
    [SerializeField] private int _maxSpawn = 6;
    private float _timeSpawn = 20f;
    private float _timer = 5f;
    private float _radius = 5f;
    private GameObject _triggerZone;

    private void Update()
    {
        if (!_isSpawn && _soliderObj.Count < _maxSpawn)
        {
            StartCoroutine(SpawnPrefab());
        }

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            RadiusSerch();
            _timer = 5f;
        }
    }

    private void OnDestroy()
    {
        if (_triggerZone != null)
        {
            _triggerZone.GetComponent<PlaneTriger>().RemoveObj(this.gameObject);
        }
    }

    private void RadiusSerch()
    {
        Collider[] overlappedCollider = Physics.OverlapSphere(transform.position, _radius);
        _soliderObj = new List<GameObject>();
        for (int i = 0; i < overlappedCollider.Length; i++)
        {
            Rigidbody rig = overlappedCollider[i].attachedRigidbody;
            if (rig)
            {
                if (rig.transform.GetComponent<GannerVSU>() != null)
                {
                    _soliderObj.Add(rig.gameObject);
                }
            }
        }
    }

    IEnumerator SpawnPrefab()
    {
        _isSpawn = true;
        yield return new WaitForSeconds(_timeSpawn);
        GameObject ad = Instantiate(_soldierprefab, _spawnSoldier.position, Quaternion.identity);
        RadiusSerch();
        _isSpawn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            _triggerZone = other.gameObject;
            _triggerZone.GetComponent<PlaneTriger>().AddObj(this.gameObject);
        }
    }
}
