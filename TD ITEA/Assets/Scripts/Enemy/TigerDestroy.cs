using UnityEngine;

public class TigerDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _gunnerPrefab;
    private int _lengthGunner = 3;

    public void TigerDestroyVoid()
    {
        for (int i = 0; i < _lengthGunner; i++)
        {
            Instantiate(_gunnerPrefab, transform.position, Quaternion.identity);
        }        
    }
}
