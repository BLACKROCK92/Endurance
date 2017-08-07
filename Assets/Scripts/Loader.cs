using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    [SerializeField]
    GameObject gameController;
    private void Awake()
    {
        if(GameController.instance == null)
        {
            Instantiate(gameController);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
