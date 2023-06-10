using System;
using System.Collections;
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
        
        public BasePlayerMovement movement;

        
        //private Health.Health _health;
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
            movement = GetComponent<BasePlayerMovement>();
            _rb = GetComponent<Rigidbody>();
            
            _rb.freezeRotation = true;
            //_health.Init(OnDeath, OnRevive);
        }

        private void Start()
        {
            _inputSignatory = GetComponent<InputSignatory>();
            _cameraMovement.Init(Camera.main);
            movement.Init(_rb, animator, _inputSignatory);
        }

        private void HealByPotion()
        {
            StartCoroutine(UseParticleForHeal());
        }

        private IEnumerator UseParticleForHeal()
        {
            //_particleSystem.Play();
            yield return new WaitForSeconds(2);
            //_particleSystem.Stop();
        }
        
        private void LateUpdate()
        {
            transform.rotation = _cameraMovement.RotateCamera(_inputSignatory.LookDirection, transform.rotation, _inputSignatory.IsMoving);
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
            
            if (!_inputSignatory.IsDashing)
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

        private void OnDeath()
        {
            animator.SetTrigger(_isDead);
            this.enabled = false;
            _inputSignatory.enabled = false;
        }
        
        private void OnRevive()
        {
            animator.SetTrigger(_isReviveing);
            this.enabled = true;
        }
    }
}