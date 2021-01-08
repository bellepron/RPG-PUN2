using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * 300 * Time.deltaTime, 0);
        Controls();
        Attack();
    }

    public void Controls()
    {

        if (Input.GetKey(KeyCode.W))
        {
            //characterController.Move(new Vector3(0, 0, 10 * Time.deltaTime));
            transform.Translate(0, 0, 10 * Time.deltaTime);
            if (!Input.GetMouseButton(0))
                animator.SetLayerWeight(2, 1);
            animator.SetLayerWeight(3, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //characterController.Move(new Vector3(0, 0, -10 * Time.deltaTime));
            transform.Translate(0, 0, -10 * Time.deltaTime);
            if (!Input.GetMouseButton(0))
                animator.SetLayerWeight(2, 1);
            animator.SetLayerWeight(3, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //characterController.Move(new Vector3(-10 * Time.deltaTime, 0, 0));
            transform.Translate(-10 * Time.deltaTime, 0, 0);
            if (!Input.GetMouseButton(0))
                animator.SetLayerWeight(2, 1);
            animator.SetLayerWeight(3, 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //characterController.Move(new Vector3(10 * Time.deltaTime, 0, 0));
            transform.Translate(10 * Time.deltaTime, 0, 0);
            if (!Input.GetMouseButton(0))
                animator.SetLayerWeight(2, 1);
            animator.SetLayerWeight(3, 1);
        }
        else
        {
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 0);
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            //animator.SetTrigger("attack");
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
}
