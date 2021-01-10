using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class HealthController : MonoBehaviour
{
    public float health = 100f;

    void Start()
    {

    }


    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            gameObject.GetComponent<PhotonView>().RPC("ApplyDamage", RpcTarget.All, 50f);
        }
    }
    
    [PunRPC]
    public void ApplyDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else
        {
            damage = health;
            health = 0;
        }

        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
