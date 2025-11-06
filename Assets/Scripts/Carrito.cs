using System;
using UnityEngine;

public class Carrito : MonoBehaviour
{
    [SerializeField] private Transform carrito; 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveDistance = 1f; 
    
    private float _currentSpeed;
    private bool _isCartOn;
    private Vector3 _startPosition;
    private bool _movingForward = true;

    private void Start()
    {
        _startPosition = carrito.position;
    }

    private void OnEnable()
    {
        EventManager.Subscribe(GlobalEvents.SwitchOn, SwitchCartOn);
        EventManager.Subscribe(GlobalEvents.SwitchOff, SwitchCartOff);
    }
    
    private void OnDisable()
    {
        EventManager.Unsubscribe(GlobalEvents.SwitchOn, SwitchCartOn);
        EventManager.Unsubscribe(GlobalEvents.SwitchOff, SwitchCartOff);
    }

    private void Update()
    {
        if (!_isCartOn)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, Time.deltaTime * 2f);
            return;
        }

        _currentSpeed = Mathf.Lerp(_currentSpeed, moveSpeed, Time.deltaTime * 2f);

        // Calcular dirección
        float direction = _movingForward ? 1f : -1f;
        
        // Mover el carrito
        carrito.Translate(Vector3.forward * (direction * _currentSpeed * Time.deltaTime));

        // Verificar si debe cambiar de dirección
        float distanceTraveled = Vector3.Distance(_startPosition, carrito.position);
        if (distanceTraveled >= moveDistance)
        {
            _movingForward = !_movingForward;
            _startPosition = carrito.position;
        }
    }

    private void SwitchCartOn() => _isCartOn = true;
    private void SwitchCartOff() => _isCartOn = false;
}