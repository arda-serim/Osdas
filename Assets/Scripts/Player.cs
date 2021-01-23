﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    PlayerControls playerControls;

    Animator animator;
    Rigidbody rb;
    CinemachineFramingTransposer cinemachine;

    bool isDiving;

    float speed = 2;
    float stamina = 100;

    Vignette vignette;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Gameplay.Button.canceled += ctx => GameManager.Instance.startGame();

        cinemachine = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        isDiving = false;

        GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet(out vignette);

        GameManager.Instance.startGame += () =>
        {
            this.enabled = true;

            playerControls.Gameplay.Button.performed += ctx => isDiving = true;
            playerControls.Gameplay.Button.canceled += ctx =>isDiving = false;
        };

        GameManager.Instance.gameOver += () => 
        {
            //Execute osdas

            rb.velocity = Vector3.zero;
            this.enabled = false;
        };
    }

    void Update()
    {
        UIManager.Instance.stamina = this.stamina;
    }

    void FixedUpdate()
    {
        if (stamina <= 0)
        {
            isDiving = false;
        }

        if (isDiving && stamina > 0)
        {
            animator.SetBool("IsDiving", true);
            rb.velocity = Vector3.down * speed * 4 * GameManager.Instance.GameSpeed;
            cinemachine.m_SoftZoneHeight = 0.35f;
            FOVIncreaser();
            VigniteIncreaser();
            StaminaReducer();
        }

        if (!isDiving)
        {
            animator.SetBool("IsDiving", false);
            rb.velocity = Vector3.down * speed * GameManager.Instance.GameSpeed; ;
            CinemachineNormaliser();
            FOVNormaliser();
            VigniteNormaliser();
            StaminaNormaliser();
        }
    }

    void CinemachineNormaliser()
    {
        if (cinemachine.m_SoftZoneHeight > 0)
        {
            cinemachine.m_SoftZoneHeight -= 0.009f;
        }

    }
    
    void FOVIncreaser()
    {
        if (Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView < 70)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView += 0.35f;
        }
    }    
    
    void FOVNormaliser()
    {
        if (Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView > 60)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView -= 0.3f;
        }
    }    
    
    void VigniteIncreaser()
    {
        if (vignette.intensity.value < 0.15)
        {
            vignette.intensity.value += 0.003f;
        }
    }   
    
    void VigniteNormaliser()
    {
        if (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= 0.01f;
        }
    }

    void StaminaReducer()
    {
        if (stamina > 0)
        {
            stamina -= 1f;
        };
    }

    void StaminaNormaliser()
    {
        if (stamina < 100)
        {
            stamina += 1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.gameOver();
        }
    }
}