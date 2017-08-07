using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CnControls;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;
    Button fireButton;
    [SerializeField]
    float fireRate = 1f;
    [SerializeField]
    float nextFire;
    AudioSource shootSound;
    PlayerHealth health;
    private void Awake()
    {
        if (this.transform.tag == "Player")
        {
            fireButton = GameObject.Find("FireButton").GetComponent<Button>();
            fireButton.onClick.AddListener(delegate { CmdFire(); });
            shootSound = GetComponent<AudioSource>();
            health = GetComponent<PlayerHealth>();
        }
        else if (this.transform.tag == "Enemy")
        {
            GetComponent<EnemyNavScript>().target = GameObject.Find("Monolith").transform;
         }

    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = CnInputManager.GetAxis("Horizontal") * Time.deltaTime * 200.0f;
        var z = CnInputManager.GetAxis("Vertical") * Time.deltaTime * 4.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
        //Vector3 movement = new Vector3(CnInputManager.GetAxis("Horizontal") * 200f, 0f, CnInputManager.GetAxis("Vertical") * 4f);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CmdFire();
        }
    }

    [Command]
    public void CmdFire()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
        shootSound.Play();
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        transform.position = new Vector3(0f, 1f, -10f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            health.TakeDamage(0.05f);
        }
    }
}