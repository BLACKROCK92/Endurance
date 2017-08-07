using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MonolithController : NetworkBehaviour
{

    public const float monolithMaxHP = 300;
    [SyncVar(hook = "UpdateMonolithSlider")]
    public float monolithHP = monolithMaxHP;
    Slider monolithSlider;
    float enemyDamage = 0.1f;
    Text monolithText;

    private void Start()
    {
        monolithSlider = GameObject.Find("MonolithHPSlider").GetComponent<Slider>();
        monolithText = GameObject.Find("MonolithHPValueTxt").GetComponent<Text>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            CmdMonolithTakeDamage(enemyDamage);
        }
    }

    [Command]
    void CmdMonolithTakeDamage(float amount)
    {
        monolithHP -= amount;
    }

    void UpdateMonolithSlider(float monoHP)
    {
        monoHP = monolithHP;
        monolithSlider.value = monoHP;
        var monolithhpvalueint = (int)monoHP;
        monolithText.text = monolithhpvalueint.ToString() + " / 300";
    }

    void Update()
    {
        if (monolithHP <= 0)
        {
            monolithHP = 0;
            Time.timeScale = 0;
        }
    }
}
