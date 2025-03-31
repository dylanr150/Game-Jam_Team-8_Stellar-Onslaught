using UnityEngine;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

public class InputManager : SingletonMonoBehavior<InputManager>
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent StopShoot = new UnityEvent();

    public float fireDelay = 0.25f;
    float cooldownTimer = 0;
    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            input += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector2.right;
        }


        OnMove?.Invoke(input);
        

        cooldownTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && cooldownTimer <= 0)
        {
            cooldownTimer = fireDelay;
            OnShoot?.Invoke();
        }
        else
        {
            StopShoot?.Invoke();
        }
        
        
    }
}
