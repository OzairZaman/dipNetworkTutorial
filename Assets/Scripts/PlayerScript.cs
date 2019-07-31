using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{

    #region Variables
    Rigidbody rigid;
    public float movementSpeed = 10.0f;
    public float rotatoinSpeed = 10.0f;
    public float jumpHeight = 2.0f;
    bool isGrounded = false;
    #endregion

    #region Methods
    void Move(KeyCode _key)
    {
        Vector3 position = rigid.position;
        Quaternion rotatoin = rigid.rotation;

        switch (_key)
        {

            case KeyCode.W:
                position += transform.forward * movementSpeed * Time.deltaTime;
                break;

            case KeyCode.S:
                position += -transform.forward * movementSpeed * Time.deltaTime;
                break;

            case KeyCode.A:
                //rotatoin *= Quaternion.AngleAxis(-rotatoinSpeed, Vector3.up);
                position += -transform.right * movementSpeed * Time.deltaTime;
                break;

            case KeyCode.D:
                //rotatoin *= Quaternion.AngleAxis(rotatoinSpeed, Vector3.up);
                position += transform.right * movementSpeed * Time.deltaTime;
                break;

            
        }
        rigid.MovePosition(position);
        rigid.MoveRotation(rotatoin);
    }

    void HandleInput()
    {
        KeyCode[] keys =
        {
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.Space
        };

        foreach (var key in keys)
        {
            if (Input.GetKey(key))
            {
                Move(key);
            }
        }
    }
    #endregion

    #region Unity
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        //get audio listener from Camera
        AudioListener audioListener = GetComponentInChildren<AudioListener>();
        //get camera
        Camera camera = GetComponentInChildren<Camera>();

        if (isLocalPlayer)
        {
            // enable everything
            camera.enabled = true;
            audioListener.enabled = true;
        }
        else
        {
            //disable everything
            camera.enabled = false;
            audioListener.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            HandleInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    #endregion

}
