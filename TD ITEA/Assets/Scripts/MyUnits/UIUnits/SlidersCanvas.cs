using UnityEngine;
using UnityEngine.UI;

public class SlidersCanvas : MonoBehaviour
{
    [SerializeField] private Slider _sliderHp;
    [SerializeField] private Slider _sliderExp;
    [SerializeField] private Item _item;

    private void Start()
    {
        _sliderExp.maxValue = _item.MaxExp;
        _sliderHp.maxValue = _item.Health;
        _sliderHp.value = _item.Health;
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