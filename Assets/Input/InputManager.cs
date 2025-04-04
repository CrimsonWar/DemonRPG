using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
  public static Vector2 Movement;
  public static bool Interact;
  public static bool ExitPressed;
 
  private PlayerInput _playerInput;
  private InputAction _moveAction;
  private InputAction _exitAction;
  private InputAction _interactAction;

  private void Awake()
  {
    _playerInput = GetComponent<PlayerInput>();

    _moveAction = _playerInput.actions["Move"];
    _exitAction = _playerInput.actions["Exit"];
    _interactAction = _playerInput.actions["Interact"];
  }

  private void Update()
  {
    Movement = _moveAction.ReadValue<Vector2>();

    if(_exitAction.triggered)
    {
      //Application.Quit();
      //Debug.Log("Exit!");
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
  }
}
