using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class W_WeaponController : MonoBehaviour
{
    int attackIndex;

    public bool canAttack = true;
    bool isStrafe = false;
    Animator animator;
    W_Controller w_Controller;
    W_IKLook w_IKLook;
    PhotonView photonView;

    public GameObject handWeapon;
    public GameObject backWeapon;

    public BoxCollider swordCollider;
    float castTime=1f;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        w_Controller = GetComponent<W_Controller>();
        w_IKLook = GetComponent<W_IKLook>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            animator.SetBool("isStrafe", isStrafe);

            if (Input.GetKeyDown(KeyCode.F))
            {
                isStrafe = !isStrafe;
            }

            if (!isStrafe)
                swordCollider = null;

            if (isStrafe)
                Attack();

            if (isStrafe == true)
            {
                w_Controller.movementType = W_Controller.MovementType.Strafe;
                w_IKLook.CloseIKSlightly();
            }

            if (isStrafe == false)
            {
                w_Controller.movementType = W_Controller.MovementType.Directional;
                w_IKLook.OpenIKSlightly();
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isStrafe && canAttack)
        {
            swordCollider = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
            canAttack = false;
            swordCollider.enabled = true;

            if (w_Controller.isStrafeMoving)
            {
                attackIndex = Random.Range(3, 5);
                animator.SetInteger("attackIndex", attackIndex);
                animator.SetTrigger("attack");
            }
            else if (!w_Controller.isStrafeMoving)
            {
                attackIndex++;
                if (attackIndex > 2) attackIndex = 0;
                animator.SetInteger("attackIndex", 0);
                animator.SetTrigger("attack");
            }
            if(attackIndex==0)
                castTime=1.08f;

                StartCoroutine(calmDown(castTime));
        }
    }

    void Equip()
    {
        if (photonView.IsMine == true)
        {
            backWeapon.SetActive(false);
            handWeapon.SetActive(true);
        }
    }

    void UnEquip()
    {
        if (photonView.IsMine == true)
        {
            backWeapon.SetActive(true);
            handWeapon.SetActive(false);
        }
    }





    IEnumerator calmDown(float calmTime)
    {
        yield return new WaitForSeconds(calmTime);
        canAttack = true;
        swordCollider.enabled = false;
    }
}
