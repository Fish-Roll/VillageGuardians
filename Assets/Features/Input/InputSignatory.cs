using Features.Attack;
using Features.Attack.Abstract;
using Features.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Input
{
    public class InputSignatory : MonoBehaviour
    {
        private InputActions _inputActions;
        
        private Vector3 _moveDirection;
        private Vector2 _lookDirection;

        private MoveController _moveController;
        private BaseAttackController _attackController;
        
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Interaction.Interaction interaction;
        
        public Vector3 MoveDirection
        {
            get => _moveDirection;
            set => _moveDirection = value;
        }
        public bool IsMoving { get; set; }
        [field:SerializeField] public bool IsDashing { get; set; }
        public bool IsAiming { get; private set; }
        public Vector2 LookDirection => _lookDirection;
        
        private void Awake()
        {
            _moveController = GetComponent<MoveController>();
            _attackController = GetComponent<BaseAttackController>();            

            Cursor.lockState = CursorLockMode.Locked;
            
            if (_inputActions != null) return;
            
            _inputActions = new InputActions();
            
            SubscribeInput();
        }

        private void SubscribeInput()
        {
            _inputActions.Controls.Walk.performed += OnWalk;
            _inputActions.Controls.Dash.performed += OnDash;
            
            _inputActions.Controls.Look.performed += OnLook;
            _inputActions.Controls.Look.canceled += OnLook;
            _inputActions.Controls.Aim.performed += OnAim;

            _inputActions.Controls.Interact.performed += OnInteract;
            
            _inputActions.Controls.LightAttack.performed += OnLightAttack;
            _inputActions.Controls.HeavyAttack.performed += OnHeavyAttack;
            _inputActions.Controls.UltimateAttack.performed += OnUltimateAttack;
        }
        private void OnEnable()
        {
            _inputActions.Controls.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Controls.Walk.performed -= OnWalk;
            _inputActions.Controls.Dash.performed -= OnDash;
            
            _inputActions.Controls.Look.performed -= OnLook;
            _inputActions.Controls.Look.canceled -= OnLook;
            _inputActions.Controls.Aim.performed -= OnAim;
            
            _inputActions.Controls.LightAttack.performed -= OnLightAttack;
            _inputActions.Controls.HeavyAttack.performed -= OnHeavyAttack;
            _inputActions.Controls.UltimateAttack.performed -= OnUltimateAttack;

            _inputActions.Controls.Disable();
        }
        
        private void OnWalk(InputAction.CallbackContext obj)
        {
            ConvertInputDirectionToMove(obj.ReadValue<Vector2>());
            IsMoving = _moveDirection.x != 0 || _moveDirection.z != 0;
        }

        private void OnDash(InputAction.CallbackContext obj)
        {
            if (IsDashing) return;
            IsDashing = true;
            StartCoroutine(_moveController._movement.Dash(_moveDirection));
        }
        
        private void OnLook(InputAction.CallbackContext obj)
        {
            _lookDirection = obj.ReadValue<Vector2>();
        }

        private void OnAim(InputAction.CallbackContext obj)
        {
            if (!IsAiming)
            {
                animator.SetLayerWeight(1, 1);
                IsAiming = true;
            }
            else
            {
                animator.SetLayerWeight(1, 0);
                IsAiming = false;
            }
        }

        private void OnInteract(InputAction.CallbackContext obj)
        {
            interaction.Interact();
        }
        
        private void OnUltimateAttack(InputAction.CallbackContext obj)
        {
            
        }
        
        private void OnLightAttack(InputAction.CallbackContext obj)
        {
            _attackController.HandleAttack((int)AttackType.LightAttack);
        }

        private void OnHeavyAttack(InputAction.CallbackContext obj)
        {
            _attackController.HandleAttack((int)AttackType.HeavyAttack);
        }
        
        private void ConvertInputDirectionToMove(Vector2 inputMoveDirection)
        {
            _moveDirection.x = inputMoveDirection.normalized.x;
            _moveDirection.z = inputMoveDirection.normalized.y;
        }
        
        public Vector3 GetMouseHitVector()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Physics.Raycast(ray, out RaycastHit hit, layerMask);
            return hit.point;
        }
    }
}
