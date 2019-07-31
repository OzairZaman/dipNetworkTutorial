using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    #region Variables
    //amount of bullets
    public float fireRate = 1f;
    public float range = 100f;
    //layer mask of which layer to hit
    public LayerMask mask;

    //private shit
    //timer for fire rate
    private float fireFactor = 0f;
    //reference to the camera child
    private GameObject camera;
    #endregion

    #region Methods
    #endregion

    #region Unity
    void Start()
    {
        Camera cam = GetComponentInChildren<Camera>();
        if (cam)
        {
            camera = cam.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

}
