using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBlock : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Player") || other.transform.CompareTag("PlayerActive"))
        {
            gameObject.tag = "TargetFull";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player") || other.transform.CompareTag("PlayerActive"))
        {
            gameObject.tag = "TargetFree";
        }
    }
}
