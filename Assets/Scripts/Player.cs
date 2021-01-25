using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    bool staminaController;

    float speed = 4;
    float stamina = 100;

    Vignette vignette;

    new ParticleSystem particleSystem;

    void Awake()
    {
        //Assigning objects or components
        cinemachine = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        animator = this.gameObject.GetComponent<Animator>();
        rb = this.gameObject.GetComponent<Rigidbody>();

        isDiving = false;
        staminaController = false;

        GameObject.Find("Global Volume").GetComponent<Volume>().profile.TryGet(out vignette);

        particleSystem = GameObject.Find("Particle System").GetComponent<ParticleSystem>();

        //Assigning actions
        GameManager.Instance.startGame += () =>
        {
            this.enabled = true;
        };

        GameManager.Instance.gameOver += () => 
        {
            particleSystem.Play();
            this.gameObject.GetComponent<AudioSource>().Play();

            rb.velocity = Vector3.zero;
            this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
            this.enabled = false;
        };
    }

    void Update()
    {
        UIManager.Instance.stamina = this.stamina;

        if (GameManager.Instance.gameRunning && Input.touchCount > 0 && !staminaController)
        {
            isDiving = true;
        }
        else
        {
            isDiving = false;
        }

        if (stamina > 30)
        {
            staminaController = false;
        }
        else if (stamina <= 0)
        {
            staminaController = true;
        }
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
            rb.velocity = Vector3.down * speed * 2 * GameManager.Instance.GameSpeed;
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

    #region Dive things

    /// <summary>
    /// Makes cinemachine's softzone normal
    /// </summary>
    void CinemachineNormaliser()
    {
        if (cinemachine.m_SoftZoneHeight > 0)
        {
            cinemachine.m_SoftZoneHeight -= 0.009f;
        }

    }
    
    /// <summary>
    /// Increases FOV if player dives
    /// </summary>
    void FOVIncreaser()
    {
        if (Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView < 70)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView += 0.6f;
        }
    }    
    
    /// <summary>
    /// Decrease FOV if player cancels diving
    /// </summary>
    void FOVNormaliser()
    {
        if (Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView > 60)
        {
            Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens
            .FieldOfView -= 0.3f;
        }
    }    
    
    /// <summary>
    /// Make vignette effect on the camera if player dives
    /// </summary>
    void VigniteIncreaser()
    {
        if (vignette.intensity.value < 0.15)
        {
            vignette.intensity.value += 0.003f;
        }
    }   
    
    /// <summary>
    /// Normalise vignette effect on the camera if player cancels diving
    /// </summary>
    void VigniteNormaliser()
    {
        if (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= 0.01f;
        }
    }

    /// <summary>
    /// Reduces stamina if player dives
    /// </summary>
    void StaminaReducer()
    {
        if (stamina > 0)
        {
            stamina -= 1f;
        };
    }

    /// <summary>
    /// Increase stamina if player is not diving
    /// </summary>
    void StaminaNormaliser()
    {
        if (stamina < 100)
        {
            stamina += 1f;
        }
    }

    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && GameManager.Instance.gameRunning)
        {
            GameManager.Instance.gameOver();
        }
    }
}