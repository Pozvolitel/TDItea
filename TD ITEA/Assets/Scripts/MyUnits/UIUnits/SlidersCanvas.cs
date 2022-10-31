using UnityEngine;
using UnityEngine.UI;

public class SlidersCanvas : MonoBehaviour
{
    [SerializeField] private Slider _sliderHp;
    [SerializeField] private Slider _sliderExp;
    [SerializeField] private int _maxValueExp;
    [SerializeField] private int _maxValueHp;

    private void Awake()
    {
        _sliderExp.maxValue = _maxValueExp;
        _sliderHp.maxValue = _maxValueHp;
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


