using UnityEngine;

public class DragShopObject : MonoBehaviour
{
    public LayerMask layer;
    private CameraController _cameraController;
    [SerializeField] private GameObject _prefabPosition;
    [SerializeField] private GameObject _shop;

    private void Start()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (_prefabPosition)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _cameraController.BoolIsDragCam(true);
                _prefabPosition.GetComponent<InstatiateShopObj>().IsBilding();
                _prefabPosition = null;
                _shop.SetActive(true);
            }
        }

        StartPreview();
    }

    public void PrefabPosition(GameObject objectprefab)
    {
        _prefabPosition = objectprefab;
    }

    private void StartPreview()
    {
        if (_prefabPosition)
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, layer))
            {
                BildZone(hitInfo);
                _prefabPosition.GetComponent<InstatiateShopObj>().IsBuild(true);
            }
            else
            {
                _prefabPosition.GetComponent<InstatiateShopObj>().IsBuild(false);
            }
        }
    }

    private void BildZone(RaycastHit hit)
    {
        if(hit.transform != null)
        {
            _prefabPosition.transform.position = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);            
        }
    }
}
