using UnityEngine;
using UnityEngine.UI;

public class CaseButton : MonoBehaviour
{
    private Button _button;
    [SerializeField] private GameObject _prefabButton;
    private CameraController _cameraController;
    [SerializeField] private GameObject _shop;

    private void Start()
    {
        _button = GetComponent<Button>();
        _cameraController = FindObjectOfType<CameraController>();
        //_button.onClick.AddListener(OnClikButton);
    }

    public void OnClikButton()
    {
        _cameraController.BoolIsDragCam(false);
        Instantiate(_prefabButton);
        _shop.SetActive(false);
    }
}
