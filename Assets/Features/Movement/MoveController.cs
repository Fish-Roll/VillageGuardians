using System;
using System.Collections;
using Features.Health;
using Features.Input;
using UnityEngine;

namespace Features.Movement
{
    [RequireComponent(typeof(InputSignatory),
        typeof(Rigidbody),
        typeof(Movement))]
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private Transform modelRotation;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float healParticleDuration;
        private Health.Health _health;
        private InputSignatory _inputSignatory;
        private Rigidbody _rb;
        private Movement _movement;
        private Vector3 _moveDirection;

        private int _walkHash;
        private int _isDead;
        private int _isReviveing;
        private void Awake()
        {
            _walkHash = Animator.StringToHash("RunAnimation");
            _isDead = Animator.StringToHash("IsDead");
            _isReviveing = Animator.StringToHash("IsRevival");
            HealthPotion.OnHealByPotion += HealByPotion;
            _health = GetComponent<Health.Health>();
            _inputSignatory = GetComponent<InputSignatory>();
            _movement = GetComponent<Movement>();
            _rb = GetComponent<Rigidbody>();
            
            _rb.freezeRotation = true;
            _health.Init(OnDeath, OnRevive);
            _cameraMovement.Init(Camera.main);
            _movement.Init(_rb, animator);
        }

        private void HealByPotion()
        {
            StartCoroutine(UseParticleForHeal());
        }

        private IEnumerator UseParticleForHeal()
        {
            _particleSystem.Play();
            yield return new WaitForSeconds(healParticleDuration);
            _particleSystem.Stop();
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
            _movement.Move(_moveDirection);
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
            _movement.enabled = false;
        }

        private void OnRevive()
        {
            animator.SetTrigger(_isReviveing);
            _movement.enabled = true;
        }
    }
}