using UnityEngine;
using UnityEngine.UI;

public class SlidersCanvas : MonoBehaviour
{
    [SerializeField] private Slider _sliderHp;
    [SerializeField] private Slider _sliderExp;
    [SerializeField] private Item _item;
    [SerializeField] private GameObject[] _star;

    private void Start()
    {
        _sliderExp.maxValue = _item.MaxExp;
        _sliderHp.maxValue = _item.Health;
        _sliderHp.value = _item.Health;
        if(_item.Level == 1)
        {
            _star[1].SetActive(false);
            _star[2].SetActive(false);
        }
        else if(_item.Level == 2)
        {
            _star[2].SetActive(false);
        }
    }

    public void HpValue(int hpValue)
    {
        _sliderHp.value = hpValue;
    }

    public void ExpValue(int expValue)
    {
        _sliderExp.value = expValue;
    }
}