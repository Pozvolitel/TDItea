using UnityEngine;

public class PlaneTriger : MonoBehaviour
{
    [SerializeField] private GameObject _obj;


    public void AddObj(GameObject obj)
    {
        _obj = obj;
        gameObject.layer = 3;
    }

    public void RemoveObj(GameObject obj)
    {
        if (obj)
        {
            _obj = null;
            gameObject.layer = 6;
        }
    }
}
