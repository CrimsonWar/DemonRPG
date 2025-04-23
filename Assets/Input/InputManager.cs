using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
  public static Vector2 Movement;
  public static bool Interact;
  public static bool ExitPressed;
  public static bool EPressed;
  public static bool QPressed;
  public static bool SpacePressed;
 
  private PlayerInput _playerInput;
  private InputAction _moveAction;
  private InputAction _exitAction;
  private InputAction _interactAction;
  private InputAction _QTE_E_Action;
  private InputAction _QTE_Q_Action;
  private InputAction _QTE_Space_Action;

  private void Awake()
  {
    _playerInput = GetComponent<PlayerInput>();

    _moveAction = _playerInput.actions["Move"];
    _exitAction = _playerInput.actions["Exit"];
    _interactAction = _playerInput.actions["Interact"];
    _QTE_E_Action = _playerInput.actions["QTE_E"];
    _QTE_Q_Action = _playerInput.actions["QTE_Q"];
    _QTE_Space_Action = _playerInput.actions["QTE_Space"];
  }

  private void Update()
  {
    Movement = _moveAction.ReadValue<Vector2>();

    if(_exitAction.triggered)
    {
      ExitPressed = true;
    } else
    {
      ExitPressed = false;
    }
    if (_interactAction.triggered)
    {
      Interact = true;
    } else
    {
      Interact = false;
    }
    if (_QTE_E_Action.triggered)
    {
      EPressed = true;
    } else
    {
      EPressed = false;
    }
    if (_QTE_Q_Action.triggered)
    {
      QPressed = true;
    } else
    {
      QPressed = false;
    }
    if (_QTE_Space_Action.triggered)
    {
      SpacePressed = true;
    } else
    {
      SpacePressed = false;
    }
  }
}
