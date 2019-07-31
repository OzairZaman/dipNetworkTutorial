using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LookScript : NetworkBehaviour
{
    #region Variables
    //public
    //sensitivity of the mouse
    [Header("Mouse properties")]
    public float mouseSensitivity = 20.0f;
    // min and max mouse vertical movement
    public float minY = -60f;
    public float maxY = 60f;

    //private
    //Yaw of the camera (Rotatoin on Y)
    private float yaw = 0f;
    //pitch of the camera (Rotation on X)
    private float pitch = 0f;
    //main camera reference
    private GameObject mainCamera;

    
    #endregion

    #region Methods
    void HandleInput()
    {
        //usually 
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minY, maxY);

    }
    #endregion

    #region Unity
    void Start()
    {
        //we gonna lock mouse
        Cursor.lockState = CursorLockMode.Locked;
        //hide the cursor
        Cursor.visible = false;
        //get a reference to the camera Gameobject 
        //      we do this by first getting the camera component
        //      and if that exists then get the object thta component is attached to
        Camera cam = GetComponentInChildren<Camera>();
        if (cam)
        {
            mainCamera = cam.gameObject;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void LateUpdate()
    {

        if (isLocalPlayer)
        {
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
            transform.localEulerAngles = new Vector3(0, yaw, 0);
        }
        
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #endregion

}
