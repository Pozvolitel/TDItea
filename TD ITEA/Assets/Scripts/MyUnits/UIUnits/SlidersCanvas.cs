using UnityEngine;
using UnityEngine.UI;

public class SlidersCanvas : MonoBehaviour
{
    [SerializeField] private Slider _sliderHp;
    [SerializeField] private Slider _sliderExp;
        
    private void Start()
    {
        _sliderExp.maxValue = GetComponentInParent<Experians>().MaxExpLv1;
        _sliderHp.maxValue = GetComponentInParent<ItemObject>().Health;
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