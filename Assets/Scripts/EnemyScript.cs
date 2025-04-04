using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed= 1f;
    public GameObject sceneLoader;
    public Transform player;
    [SerializeField] private GameObject EncounterSlot1;
    [SerializeField] private GameObject EncounterSlot2;
    [SerializeField] private GameObject EncounterSlot3;
    
    private Vector2 _movement;
    private Vector3 _startPos;
    private Vector3 _roamPos;
    private bool resetDone = true;
    private bool seesPlayer = false;

    private Rigidbody2D _rb;
    private Animator _animator;
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    enum EnemyState
    {
        Idle,
        Hunt,
        Reset
    }
    EnemyState currentState;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _startPos = transform.position;
        _roamPos = GetRoamingPosition();
    }

    void Update()
    {
        if(overworldManager.inst.state == overworldState.Play) {
            player = overworldManager.inst.PartyObj.transform;
            findTarget();
            if (seesPlayer)
            {
                currentState = EnemyState.Hunt;
            } 
            else
            {
                if(currentState == EnemyState.Hunt) {
                    if(!resetDone) {
                        currentState = EnemyState.Reset;
                    } else {
                        currentState = EnemyState.Idle;
                    }
                } else {
                    currentState = EnemyState.Idle;
                }
                
            }
            UpdateState();
            _animator.SetFloat(_horizontal, _movement.x);
            _animator.SetFloat(_vertical, _movement.y);
        }
    }

    void UpdateState()
    {
        float reachedDistance = 1f;
        switch (currentState)
        {
            case EnemyState.Idle:
                MoveTo(_roamPos);
                if (Vector3.Distance(transform.position, _roamPos) < reachedDistance)
                {
                    _roamPos = GetRoamingPosition();                
                }
                break;
            case EnemyState.Hunt:
                huntPlayer();
                break;
            case EnemyState.Reset:
                resetDone = false;
                MoveTo(_startPos);
                if (Vector3.Distance(transform.position, _startPos) < reachedDistance)
                {
                    resetDone = true;
                    _roamPos = GetRoamingPosition();                
                }
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            encounterCache.Cache.setEncounter(this.gameObject, EncounterSlot1, EncounterSlot2, EncounterSlot3);
            PartyManager.inst.setOverworldPos(other.gameObject.transform.position);
            sceneLoader.GetComponent<sceneLoader>().loadBattle();
        } 
    }

    private Vector3 GetRoamingPosition()
    {
        return  _startPos + UtilsClass.GetRandomDir() * Random.Range(1f,4f);
    }

    public void MoveTo(Vector3 movePosition)
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        _movement.Set(moveDir.x, moveDir.y);
        _rb.velocity = _movement * moveSpeed;
    }

    void huntPlayer()
    {
        MoveTo(player.position);
    }

    void findTarget()
    {
        float viewRange = 2.15f;
        if(Vector3.Distance(transform.position, player.position) < viewRange) {
            /*Vector3 dirToPlayer = (player.position - transform.position).normalized;
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, dirToPlayer, viewRange);
            if(raycastHit.collider != null) {
                if (raycastHit.collider.gameObject.tag == "Player")
                {
                    seesPlayer = true;
                }
                else
                {
                    seesPlayer = false;
                }
            }
            else
            {
                seesPlayer = false;
            }
        }
        else
        {
            seesPlayer = false;*/
            seesPlayer = true;
        }
        else
        {
            seesPlayer = false;
        }
    }


}
