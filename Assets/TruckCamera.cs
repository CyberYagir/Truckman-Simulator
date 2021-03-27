using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckCamera : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame

    public bool Can()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag("Normal");
    }
    public void Back()
    {
        animator.SetBool("Back", false);
    }
    void Update()
    {
        if (InputManager.GetKey("LookLeft").isDown)
        {
            if (animator.GetBool("Back") == false)
            {
                animator.Play("CabineLeft");
                animator.SetBool("Back", true);
            }
            else
            {
                animator.SetBool("Back", false);
            }
        }
        else if (InputManager.GetKey("LookRight").isDown)
        {
            if (animator.GetBool("Back") == false)
            {
                animator.Play("CabineRight");
                animator.SetBool("Back", true);
            }
            else
            {
                animator.SetBool("Back", false);
            }
        }
    }
}
