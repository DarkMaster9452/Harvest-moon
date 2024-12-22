using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnim : MonoBehaviour
{
    private Animator animator;
    public float speed = 5.0f; // Movement speed

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Reset all animation booleans to false
        ResetAnimations();

        Vector3 movement = Vector3.zero;

        // Check for movement and set the appropriate animation
        if (Input.GetKey(KeyCode.W))
        {
            SetAnimationState("anim. up");
            movement += Vector3.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetAnimationState("anim. down");
            movement += Vector3.down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SetAnimationState("anim. left");
            movement += Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetAnimationState("anim. right");
            movement += Vector3.right;
        }
        else
        {
            SetAnimationState("anim. idle");
        }

        // Apply movement
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void ResetAnimations()
    {
        animator.SetBool("anim. up", false);
        animator.SetBool("anim. down", false);
        animator.SetBool("anim. left", false);
        animator.SetBool("anim. right", false);
        animator.SetBool("anim. idle", false);
    }

    private void SetAnimationState(string state)
    {
        animator.SetBool(state, true);
    }

    // Ensure the Animator component is attached to the GameObject
    void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
}