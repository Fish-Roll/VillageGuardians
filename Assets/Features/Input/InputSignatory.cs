using Features.Attack;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Input
{
    public class InputSignatory : MonoBehaviour
    {
        private InputActions _inputActions;
        
        private Vector3 _moveDirection;
        private Vector2 _lookDirection;

        private GirlAttackController _girlAttackController;
        private BoyAttackController _boyAttackController;
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask layerMask;
        public Vector3 MoveDirection
        {
            get => _moveDirection;
            set => _moveDirection = value;
        }
        public bool IsMoving { get; set; }
        public bool IsAiming { get; private set; }
        public Vector2 LookDirection => _lookDirection;
        
        private void Awake()
        {
           
            Cursor.lockState = CursorLockMode.Locked;
            if (!TryGetComponent(out _girlAttackController))
            {
                _boyAttackController = GetComponent<BoyAttackController>();
            }
            
            if (_inputActions != null) return;
            
            _inputActions = new InputActions();
            _inputActions.Controls.Walk.performed += OnWalk;
            _inputActions.Controls.Look.performed += OnLook;
            _inputActions.Controls.Look.canceled += OnLook;
            _inputActions.Controls.Aim.performed += OnAim;

            _inputActions.Controls.LightAttack.started += OnLightAttack;
            _inputActions.Controls.LightAttack.performed += OnLightAttack;
            _inputActions.Controls.LightAttack.canceled += OnLightAttack;
            
            _inputActions.Controls.HeavyAttack.started += OnHeavyAttack;
            _inputActions.Controls.HeavyAttack.performed += OnHeavyAttack;
            _inputActions.Controls.HeavyAttack.canceled += OnHeavyAttack;

        }
        
        private void OnEnable()
        {
            _inputActions.Controls.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Controls.Walk.performed -= OnWalk;
            _inputActions.Controls.Look.performed -= OnLook;
            _inputActions.Controls.Look.canceled -= OnLook;
            _inputActions.Controls.Aim.performed -= OnAim;
            
            _inputActions.Controls.LightAttack.started -= OnLightAttack;
            _inputActions.Controls.LightAttack.performed -= OnLightAttack;
            _inputActions.Controls.LightAttack.canceled -= OnLightAttack;
            
            _inputActions.Controls.HeavyAttack.started -= OnHeavyAttack;
            _inputActions.Controls.HeavyAttack.performed -= OnHeavyAttack;
            _inputActions.Controls.HeavyAttack.canceled -= OnHeavyAttack;

            
            _inputActions.Controls.Disable();
        }
        
        private void OnWalk(InputAction.CallbackContext obj)
        {
            ConvertInputDirectionToMove(obj.ReadValue<Vector2>());
            IsMoving = _moveDirection.x != 0 || _moveDirection.z != 0;
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
        
        private void OnLightAttack(InputAction.CallbackContext obj)
        {
            if (_girlAttackController != null)
            {
                _girlAttackController.LightAttack(GetMouseHitVector(), IsAiming);
            }
            else
            {
                _boyAttackController.LightAttack(IsAiming);
            }
        }

        private void OnHeavyAttack(InputAction.CallbackContext obj)
        {
            if (_girlAttackController != null)
            {
                _girlAttackController.HeavyAttack(IsAiming);
            }
            else
            {
                _boyAttackController.HeavyAttack();
            }
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
