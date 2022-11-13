using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateShopObj : MonoBehaviour
{
    private DragShopObject _dragShopObject;
    [SerializeField] private GameObject _tower;

    private void Start()
    {
        _dragShopObject = Camera.main.GetComponent<DragShopObject>();
        _dragShopObject.PrefabPosition(this.gameObject);
    }

    public void IsBilding()
    {
        Instantiate(_tower, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
