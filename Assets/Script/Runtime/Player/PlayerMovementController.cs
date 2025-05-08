using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

//Responsible for controlling player's movement using Unity's CharacterController component.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float speed = 1f;
    public float gravity = -9.81f;

    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private CharacterController _controller;
    private Vector2 _moveInput, _look;
    private Vector3 _move, _velocity;
    public Camera _cam;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        RotatePlayerTowardsCamera();
        MovePlayer();
    }
    private void MovePlayer()
    {
        _move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        _controller.Move(_move * (speed * Time.deltaTime));

        if (IsGrounded() && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
    private void RotatePlayerTowardsCamera()
    {
        if (_cam != null)
        {
            Vector3 cameraForward = _cam.transform.forward;
            cameraForward.y = 0f; // Ignore the y-axis rotation

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = newRotation;
            }
        }
        else
        {
            throw new System.Exception("Camera is not assigned");
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDistance);
    }
    private void OnEnable()
    {
        InputSignals.Instance.OnInputMoveUpdate += MovementInputUpdate;
        InputSignals.Instance.OnInputeLookUpdate += LookInputUpdate;
    }
    private void OnDisable()
    {
        if (InputSignals.Instance == null) { return; }
        InputSignals.Instance.OnInputMoveUpdate -= MovementInputUpdate;
        InputSignals.Instance.OnInputeLookUpdate -= LookInputUpdate;
    }
    private void MovementInputUpdate(Vector2 data)
    {
        _moveInput = data;
    }
    private void LookInputUpdate(Vector2 data)
    {
        _look = data;
    }
}