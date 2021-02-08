using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceObj : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The object that will be faces. If none is assigned, the Camera.main will be used instead.")]
    public GameObject ObjectToFace;
    void Start()
    {
        if (ObjectToFace == null) ObjectToFace = Camera.main.gameObject;
    }
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
    }
}
