using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectDeactive : MonoBehaviour
{
    [SerializeField] GameObject obj;

    public void OnClick()
    {
        obj.SetActive(false);
    }
}
