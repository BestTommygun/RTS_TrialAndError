using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    void Start()
    {
       transform.GetChild(1).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
