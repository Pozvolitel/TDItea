using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;
    private Vector3 _newPosition;
    private float _movementTime = 30;
    [SerializeField] private Vector3 _camPosX;
    [SerializeField] private Vector3 _camPosZ;
    private bool _isDragCam = true;

    private void Start()
    {
        _newPosition = transform.position;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entry;
            if(plane.Raycast(ray, out entry))
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if (_isDragCam)
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float entry;
                if (plane.Raycast(ray, out entry))
                {
                    _dragCurrentPosition = ray.GetPoint(entry);

                    _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
                }
            }
        
            transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * _movementTime);
            transform.position = new Vector3(Mathf.Clamp(_newPosition.x, 25, 108), _newPosition.y, Mathf.Clamp(_newPosition.z, 13, 140));
        }        
    }

    public void BoolIsDragCam(bool isDragCam)
    {
        _isDragCam = isDragCam;
    }
}
