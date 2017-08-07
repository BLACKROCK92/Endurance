using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{

    public const float maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = maxHealth;
    public RectTransform healthBar;

    public void TakeDamage(float amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (transform.tag == "Enemy")
            {
                GameController.instance.CmdModifyScore(10);
                Destroy(this.gameObject, 0.1f);
                return;
            }
            currentHealth = maxHealth;
            RpcRespawn();
        }

    }

    void OnChangeHealth(float health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = new Vector3(0f, 2.5f, -10f);
        }
    }
}
