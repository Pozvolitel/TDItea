using UnityEngine;

public class InstatiateShopObj : MonoBehaviour
{
    private DragShopObject _dragShopObject;
    [SerializeField] private GameObject _tower;
    private bool _isbuild = false;
    [SerializeField] private int _cointTower;

    private void Start()
    {
        _dragShopObject = Camera.main.GetComponent<DragShopObject>();
        _dragShopObject.PrefabPosition(this.gameObject);
    }

    public void IsBuild(bool isBuild)
    {
        _isbuild = isBuild;
    }

    public void IsBilding()
    {        
        if (_isbuild)
        {
            Instantiate(_tower, transform.position, Quaternion.identity);
            FindObjectOfType<GameManager>().AddRandomScore(-_cointTower);
        }
            Destroy(gameObject);
    }
}
