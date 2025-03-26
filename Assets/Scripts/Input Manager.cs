using UnityEngine;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    void Update()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector3.right;
        }

        OnMove?.Invoke(input);
    }
}
