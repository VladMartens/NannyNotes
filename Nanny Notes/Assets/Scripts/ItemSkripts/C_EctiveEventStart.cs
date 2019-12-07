using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class C_EctiveEventStart : MonoBehaviour
{
    public UnityEvent whoa;

    private void Start()
    {
        whoa.Invoke();
    }
}
