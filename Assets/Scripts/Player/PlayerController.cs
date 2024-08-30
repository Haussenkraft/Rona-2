using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public InputAction playerControls;
    public InputAction interactControl; 

    Vector2 moveDirection = Vector2.zero;
    public float moveSpeed = 5f;

    public GameObject dialogueUI; 
    private bool isNearInteractable = false;
    private GameObject currentInteractable;

    private void OnEnable()
    {
        playerControls.Enable();
        interactControl.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        interactControl.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dialogueUI.SetActive(false); 
    }

    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();

        if (isNearInteractable && interactControl.WasPressedThisFrame())
        {
            ShowDialogue();
        }
    }

    void FixedUpdate()
    {
        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("entered interactable");
            isNearInteractable = true;
            currentInteractable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Debug.Log("exit interactable");
            isNearInteractable = false;
            currentInteractable = null;
            dialogueUI.SetActive(false); 
        }
    }

    void ShowDialogue()
    {
        dialogueUI.SetActive(true);
        
        
    }
}
