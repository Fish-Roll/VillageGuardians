using Cinemachine;
using Features.Attack;
using Features.Attack.Abstract;
using Features.Interaction;
using Features.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Input
{
    public class InputSignatory : MonoBehaviour
    {
        [SerializeField] private GameObject girlPlayer;
        [SerializeField] private CinemachineVirtualCamera girlCamera;

        [SerializeField] private GameObject boyPlayer;
        [SerializeField] private CinemachineVirtualCamera boyCamera;
        
        [Space(5)]
        private InputActions _inputActions;
        
        private Vector3 _moveDirection;
        private Vector2 _lookDirection;
        private bool _isBool;
        private MoveController _moveController;
        private BaseAttackController _attackController;
        
        [SerializeField] private Animator animator;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private InteractionController interactionController;
        public Vector3 MoveDirection
        {
            get => _moveDirection;
            set => _moveDirection = value;
        }
        public bool IsMoving { get; set; }
        [field:SerializeField] public bool IsDashing { get; set; }
        public bool IsAiming { get; private set; }
        public Vector2 LookDirection => _lookDirection;
        
        public void Awake()
        {
            _moveController = GetComponent<MoveController>();
            _attackController = GetComponent<BaseAttackController>();            

            Cursor.lockState = CursorLockMode.Locked;
            
            if (_inputActions != null) return;
            
            _inputActions = new InputActions();
            
        }

        // public void SubscribeInput()
        // {
        //     _inputActions.Controls.Walk.performed += OnWalk;
        //     _inputActions.Controls.Dash.performed += OnDash;
        //     
        //     _inputActions.Controls.Look.performed += OnLook;
        //     _inputActions.Controls.Look.canceled += OnLook;
        //     _inputActions.Controls.Aim.performed += OnAim;
        //
        //     _inputActions.Controls.Interact.performed += OnInteract;
        //     _inputActions.Controls.ChangePlayer.performed += OnChangePlayer;
        //
        //     _inputActions.Controls.LightAttack.performed += OnLightAttack;
        //     _inputActions.Controls.HeavyAttack.performed += OnHeavyAttack;
        //     _inputActions.Controls.UltimateAttack.performed += OnUltimateAttack;
        // }
        // public void OnEnable()
        // {
        //     SubscribeInput();
        //
        //     _inputActions.Controls.Enable();
        // }
        //
        // public void OnDisable()
        // {
        //     _inputActions.Controls.Walk.performed -= OnWalk;
        //     _inputActions.Controls.Dash.performed -= OnDash;
        //     
        //     _inputActions.Controls.Look.performed -= OnLook;
        //     _inputActions.Controls.Look.canceled -= OnLook;
        //     _inputActions.Controls.Aim.performed -= OnAim;
        //     
        //     _inputActions.Controls.LightAttack.performed -= OnLightAttack;
        //     _inputActions.Controls.HeavyAttack.performed -= OnHeavyAttack;
        //     _inputActions.Controls.UltimateAttack.performed -= OnUltimateAttack;
        //     _inputActions.Controls.ChangePlayer.performed -= OnChangePlayer;
        //     _inputActions.Controls.Disable();
        // }

        public void OnChangePlayer(InputAction.CallbackContext obj)
        {
            if (girlCamera.enabled)
            {
                boyPlayer.SetActive(true);
                boyCamera.enabled = true;

                girlCamera.enabled = false;
                girlPlayer.SetActive(false);
            }
            else if (boyCamera.enabled)
            {
                girlPlayer.SetActive(true);
                girlCamera.enabled = true;
                
                boyCamera.enabled = false;
                boyPlayer.SetActive(false);
            }
        }
        
        public void OnWalk(InputAction.CallbackContext obj)
        {
            ConvertInputDirectionToMove(obj.ReadValue<Vector2>());
            IsMoving = _moveDirection.x != 0 || _moveDirection.z != 0;
        }

        public void OnDash(InputAction.CallbackContext obj)
        {
            if (IsDashing) return;
            IsDashing = true;
            StartCoroutine(_moveController.movement.Dash(_moveDirection));
        }
        
        public void OnLook(InputAction.CallbackContext obj)
        {
            _lookDirection = obj.ReadValue<Vector2>();
        }

        public void OnAim(InputAction.CallbackContext obj)
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

        public void OnInteract(InputAction.CallbackContext obj)
        {
            interactionController.HandleInteraction();
        }
        
        public void OnUltimateAttack(InputAction.CallbackContext obj)
        {
            if(!_isBool)
                animator.SetTrigger("IsBoust");
        }
        
        public void OnLightAttack(InputAction.CallbackContext obj)
        {
            if(IsAiming)
                _attackController.HandleAttack((int)AttackType.LightAttack);
        }

        public void OnHeavyAttack(InputAction.CallbackContext obj)
        {
            if(!IsAiming)
                _attackController.HandleAttack((int)AttackType.HeavyAttack);
        }
        
        public void ConvertInputDirectionToMove(Vector2 inputMoveDirection)
        {
            _moveDirection.x = inputMoveDirection.normalized.x;
            _moveDirection.z = inputMoveDirection.normalized.y;
        }
        
        public Vector3 GetMouseHitVector()
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //Physics.Raycast(ray, out RaycastHit hit, layerMask);
            //Vector3 vec = new Vector3(-ray.direction.x, ray.direction.y, -ray.direction.z);
            return ray.direction;
        }
    }
}
