using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private LayerMask _detectionLayer;
    [SerializeField] private Transform _detectionPoint;
    private const float _detectionRadius = 0.4f;
    public GameObject detectedObject;
    public static bool inDialogue = false;
    public static bool Moving = false;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorzontal";
    private const string _lastVertical = "LastVertical";

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Instance = this;
    }

    private void Update()
    {
        if(overworldManager.inst.state == overworldState.Play) {
            if (!inDialogue)
            {
                if (!Moving)
                {
                    _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
                }
                _rb.velocity = _movement * _moveSpeed;

                _animator.SetFloat(_horizontal, _movement.x);
                _animator.SetFloat(_vertical, _movement.y);

                if(_movement != Vector2.zero)
                {
                    _animator.SetFloat(_lastHorizontal, _movement.x);
                    _animator.SetFloat(_lastVertical, _movement.y);
                }
            }

            if (DetectTrigger())
            {
                if (InputManager.Interact)
                {
                    if (!inDialogue)
                    {
                        inDialogue = true;
                        detectedObject.GetComponent<DialogueScript>().Interact();    
                    }
                    else
                    {
                        detectedObject.GetComponent<DialogueScript>().NextLine();
                    }                
                }
            }
        }
    }

    bool DetectTrigger()
    {
        Collider2D obj = Physics2D.OverlapCircle(_detectionPoint.position, _detectionRadius, _detectionLayer);

        if (obj==null)
        {
            detectedObject = null;
            return false;
        } 
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void MoveTo(Vector3 movePosition)
    {
        Moving = true;
        Vector3 moveDir = Vector3.zero;

        while (transform.position != movePosition)
        {
            moveDir = (movePosition - transform.position).normalized;
            _movement.Set(moveDir.x,moveDir.y);
        }
        _movement = Vector2.zero;

        Moving = false;
    }
}
