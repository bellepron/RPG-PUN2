using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_IKLook : MonoBehaviour
{
    Animator animator;
    Camera mainCamera;
    float weight = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    // private void OnAnimatorIK(int layerIndex)
    // {
    //     animator.SetLookAtWeight(weight, 0.5f * weight, 1.2f * weight, 0.5f * weight, 0.5f * weight); //weight,bodyWeight,headWeight,eyesWeight,clampWeight

    //     Ray lookAtRay = new Ray(transform.position, mainCamera.transform.forward);
    //     animator.SetLookAtPosition(lookAtRay.GetPoint(25));
    // }

    public void OpenIKSlightly()
    {
        weight = Mathf.Lerp(weight, 1f, Time.fixedDeltaTime);
    }
    public void CloseIKSlightly()
    {
        weight = Mathf.Lerp(weight, 0f, Time.fixedDeltaTime);
    }
}
