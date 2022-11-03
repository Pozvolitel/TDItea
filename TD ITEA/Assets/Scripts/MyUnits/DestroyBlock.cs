using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    [SerializeField] private GannerVSU[] _gannerVSU;
    private void OnDestroy()
    {
        for (int i = 0; i < _gannerVSU.Length; i++)
        {
            _gannerVSU[i].TransformNextTarget();
        }
    }
}
