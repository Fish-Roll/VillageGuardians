using System;
using System.Collections;
using Features.Attack.Abstract;
using Features.Attack.Boy;
using Features.Health;
using Features.Input;
using Features.Movement.Abstract;
using UnityEngine;

namespace Features.Movement
{
    [RequireComponent(typeof(InputSignatory),
        typeof(Rigidbody))]
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private Transform modelRotation;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera camera;
        
        public BasePlayerMovement movement;

        
        //private Health.Health _health;
        private BaseMeleeAttack _heavyMeleeAttack;
        private InputSignatory _inputSignatory;
        private Rigidbody _rb;
        private Vector3 _moveDirection;

        private int _walkHash;
        private int _isDead;
        private int _isReviveing;
        
        private void Awake()
        {
            _walkHash = Animator.StringToHash("RunAnimation");
            _isDead = Animator.StringToHash("Death");
            _isReviveing = Animator.StringToHash("Revival");
            
            // HealthPotion.OnHealByPotion += HealByPotion;
            
            //_health = GetComponent<Health.Health>();
            _heavyMeleeAttack = GetComponent<BaseMeleeAttack>();
            movement = GetComponent<BasePlayerMovement>();
            _rb = GetComponent<Rigidbody>();
            
            _rb.freezeRotation = true;
            //_health.Init(OnDeath, OnRevive);
        }

        private void Start()
        {
            _inputSignatory = GetComponent<InputSignatory>();
            _cameraMovement.Init(camera);
            movement.Init(_rb, animator, _inputSignatory);
        }
        
        
        private void LateUpdate()
        {
            transform.rotation = _cameraMovement.RotateCamera(_inputSignatory.LookDirection, transform.rotation, _inputSignatory.IsMoving);
            
            if (!_inputSignatory.IsDashing && _inputSignatory.IsMoving &&
                (_moveDirection.x != 0 || _moveDirection.z != 0)
                && !_inputSignatory.isHeavyAttacked
                && !_inputSignatory.isInteracting)
                RotatePlayer();
        }

        private void Update()
        {
            if (!_inputSignatory.IsMoving)
            {
                animator.SetBool(_walkHash, false);
            }
            _moveDirection = _inputSignatory.MoveDirection;
            _moveDirection = _cameraMovement.ConvertToCameraMovement(_moveDirection, _inputSignatory.MoveDirection);
        }

        private void FixedUpdate()
        {
            _rb.velocity = new Vector3(_rb.velocity.x, -10, _rb.velocity.z);
            
            if (!_inputSignatory.IsDashing && _inputSignatory.IsMoving &&
                (_moveDirection.x != 0 || _moveDirection.z != 0)
                && !_inputSignatory.isHeavyAttacked
                && !_inputSignatory.isInteracting)
            {
                animator.SetBool(_walkHash, true);
                movement.Move(_moveDirection);
            }
        }

        private void RotatePlayer()
        {
            Vector3 positionToLookAt;
            positionToLookAt.x = _moveDirection.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = _moveDirection.z;
            Quaternion curRotation = modelRotation.rotation;
            if (_inputSignatory.IsMoving)
            {
                Quaternion rotate = Quaternion.LookRotation(positionToLookAt);
                modelRotation.rotation = Quaternion.Slerp(curRotation, rotate, rotationSpeed * Time.deltaTime);
            }
        }

    }
}