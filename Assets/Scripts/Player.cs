using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    PlayerControls playerControls;

    Animator animator;
    Rigidbody rb;

    float speed = 2;

    CinemachineFramingTransposer cinemachine;

    void Awake()
    {
        cinemachine = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        #region Inputs
        playerControls = new PlayerControls();

        playerControls.Enable();

        playerControls.Gameplay.Dive.performed += ctx => 
        {
            animator.SetBool("IsDiving", true);
            rb.velocity = Vector3.down * speed * 4;
            cinemachine.m_SoftZoneHeight = 0.25f;
        };
        playerControls.Gameplay.Dive.canceled += ctx =>
        {
            animator.SetBool("IsDiving", false);
            rb.velocity = Vector3.down * speed;
            StartCoroutine(CinemachineNormaliser());
        };
        #endregion
    }

    void Update()
    {
        
    }

    IEnumerator CinemachineNormaliser()
    {
        while (cinemachine.m_SoftZoneHeight > 0)
        {
            if (playerControls.Gameplay.Dive.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
            {
                break;
            }
            cinemachine.m_SoftZoneHeight -= 0.003f;
            yield return new WaitForEndOfFrame();
        }

    }
}
