using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))] 
public class FPSController : MonoBehaviour
{

    [Header("FPS Settings")]
    private float _xRotation;
    private Vector3 _moveVector;
    private CharacterController _controller;
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Camera cameraa;
    [SerializeField] private float xCameraBounds = 60f;

    public Transform groundChecker;
    private bool _isGrounded;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _distance = 0.3f;
    private float _gravity = -10f; 
    private float _yVelocity;

    #region Smoothing code
    private Vector2 _currentMouseDelta;
    private Vector2 _currentMouseVelocity;
    [SerializeField] private float smoothTime = .1f;
    
    #endregion

    [Header("Respawn System")]
    public Transform[] respawnPoints;
    public int currentRespawnIndex;
    private float _health = 100f;
    private float _maxHealth = 100f;



    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        Rotation();
        Jump();

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Die");
            takeDamage(_health);
        }
    }

    private void Movement()
    {
        _moveVector = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        _moveVector.Normalize();

        _isGrounded = Physics.Raycast(groundChecker.position, Vector3.down, _distance);

        if (_isGrounded && _yVelocity < 0)
        {
            _yVelocity = -2f;
        }
        else
        {
            _yVelocity += _gravity * Time.deltaTime;
        }

        _moveVector.y = _yVelocity;

        _controller.Move(_moveVector * speed * Time.deltaTime);
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Vector2 targetDelta = new Vector2(mouseX, mouseY);
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetDelta, ref _currentMouseVelocity, smoothTime);
        _xRotation -= _currentMouseDelta.y;
        _xRotation = Mathf.Clamp(_xRotation, -xCameraBounds, xCameraBounds);
        transform.Rotate(Vector3.up * _currentMouseDelta.x);
        cameraa.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Debug.Log("Jump triggered");
            _yVelocity = Mathf.Sqrt(2 * _jumpForce * -_gravity);
        }


    }


    void Respawn()
    {
        if (respawnPoints.Length > 0)
        {
            Transform respawnPoint = respawnPoints[currentRespawnIndex];
            transform.position = respawnPoint.position;
            _health = _maxHealth;
            _yVelocity = 0;
        }
    }



    public void takeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Respawn();
    }
}
