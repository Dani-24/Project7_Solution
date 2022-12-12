using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerController _controller;
    private MovementController _moveController;

    private float _upDown;
    private float _speed;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        _moveController = GetComponent<MovementController>();
    }

    private void Update()
    {
        _moveController.TryMove(_speed);
    }
    // Start is called before the first frame update
    private void OnMove(InputValue input)
    {
        _speed = input.Get<float>();
       
    }

    private void OnUpDown(InputValue input)
    {
        _upDown = input.Get<float>();        
    }

    private void OnQuickAttack()
    {
        if (_upDown >= 0)
            _controller.TryHighQuickAttack();
        else
            _controller.TryLowQuickAttack();
    }
    private void OnSlowAttack()
    {
        if (_upDown >= 0)
            _controller.TryHighSlowAttack();
        else
            _controller.TryLowSlowAttack();
    }

    private void OnBlock()
    {
        if (_upDown >= 0)
            _controller.TryHighBlock();
        else
            _controller.TryLowBlock();
    }
}
