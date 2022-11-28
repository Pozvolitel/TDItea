using CustomEvents;
using UnityEngine;
using UnityEngine.UI;

public class CaseButton : MonoBehaviour
{
    private Button _button;
    [SerializeField] private GameObject _prefabButton;
    private CameraController _cameraController;
    [SerializeField] private GameObject _shop;
    [SerializeField] private int _cointTower;
    private int _cointScore;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _cameraController = FindObjectOfType<CameraController>();
        EventAggregator.Subscrible<ScorePointChanged>(ScorePoint);
    }

    private void OnDestroy()
    {
        EventAggregator.UnSubscrible<ScorePointChanged>(ScorePoint);
    }

    private void ScorePoint(object sender, ScorePointChanged eventData)
    {
        _cointScore = eventData.ScorePoint;
    }

    public void OnClikButton()
    {
        if (_cointScore >= _cointTower && FindObjectOfType<GameManager>().Build == true)
        {
            _cameraController.BoolIsDragCam(false);
            Instantiate(_prefabButton);
            _shop.SetActive(false);            
        }        
    }

    public void ColorButton(float color)
    {
        GetComponent<Image>().color = new Color(1, 1, 1, color);
    }
}
