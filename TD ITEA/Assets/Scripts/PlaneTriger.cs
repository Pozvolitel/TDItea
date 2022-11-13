using UnityEngine;

public class PlaneTriger : MonoBehaviour
{
    public void NewLayer(bool layer)
    {
        if(layer)
        {
            gameObject.layer = 3;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
