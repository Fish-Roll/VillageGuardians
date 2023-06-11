using Cinemachine;
using Features.Attack;
using Features.Attack.Abstract;
using Features.Interaction;
using Features.Movement;
using Features.Rage;
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
        
        [SerializeField] private RageController _rageController;
        [SerializeField] private string controlScheme;
        
        [Space(5)] 
        private PlayerInput _playerInput;
        //private InputActions _inputActions;
        
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
            _playerInput = GetComponent<PlayerInput>();
            _moveController = GetComponent<MoveController>();
            _attackController = GetComponent<BaseAttackController>();            

            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void OnEnable()
        {
            SubscribeInput();
        }
        
        public void SubscribeInput()
        {
            _playerInput.actions["Walk"].performed += OnWalk;
            _playerInput.actions["Dash"].performed += OnDash;
            
            _playerInput.actions["Look"].performed += OnLook;
            _playerInput.actions["Look"].canceled += OnLook;
            _playerInput.actions["Aim"].performed += OnAim;
        
            _playerInput.actions["Interact"].performed += OnInteract;
            _playerInput.actions["ChangePlayer"].performed += OnChangePlayer;
        
            _playerInput.actions["LightAttack"].performed += OnLightAttack;
            _playerInput.actions["HeavyAttack"].performed += OnHeavyAttack;
            _playerInput.actions["UltimateAttack"].performed += OnUltimateAttack;
        }

        public void OnDisable()
        {
            _playerInput.actions["Walk"].performed -= OnWalk;
            _playerInput.actions["Dash"].performed -= OnDash;
            
            _playerInput.actions["Look"].performed -= OnLook;
            _playerInput.actions["Look"].canceled -= OnLook;
            _playerInput.actions["Aim"].performed -= OnAim;
        
            _playerInput.actions["Interact"].performed -= OnInteract;
            _playerInput.actions["ChangePlayer"].performed -= OnChangePlayer;
        
            _playerInput.actions["LightAttack"].performed -= OnLightAttack;
            _playerInput.actions["HeavyAttack"].performed -= OnHeavyAttack;
            _playerInput.actions["UltimateAttack"].performed -= OnUltimateAttack;
        }

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
            if (_rageController != null) 
                _rageController.TryActivate();
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
            return ray.direction;
        }
    }
}
