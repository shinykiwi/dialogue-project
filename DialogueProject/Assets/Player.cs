using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    
    
    // Raycast variables
    private Ray ray;
    private RaycastHit raycastHit;
    private float maxRaycastDistance = 5f;
    [SerializeField] private LayerMask layersToHit;
    
    // Dialogue related variables
    private bool InDialogue = false;
    private PlayerMovement playerMovement;
    private BasicNPC npc;
    

    private void Start()
    {
        hud.SetActive(false); // hide the HUD to begin with
    }

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        CheckForColliders();
    }

    void CheckForColliders()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out raycastHit, maxRaycastDistance, layersToHit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)* maxRaycastDistance, Color.red);
            if (!InDialogue)
            {
                hud.SetActive(true);
            }
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxRaycastDistance, Color.green);
            hud.SetActive(false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (raycastHit.collider != null)
        {
            Debug.Log("E was pressed!" + raycastHit.collider.gameObject.name);
            GameObject npcGameObject = raycastHit.collider.gameObject;
            DescantDialogueTrigger dialogueTrigger = npcGameObject.GetComponent<DescantDialogueTrigger>();
            npc = npcGameObject.GetComponent<BasicNPC>();
            
            // If this component exists on the target
            if (dialogueTrigger)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (!npc.IsInDialogue())
                {
                    playerMovement.DisableMovement();
                    dialogueTrigger.Display();
                    npc.SetInDialogue(true);
                    InDialogue = true;
                }
            }
            
        }
    }

    // Called in Descant Graphs in the End Choice node
    public void ExitDialogue()
    {
        playerMovement.EnableMovement();
        npc.SetInDialogue(false);
        InDialogue = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
