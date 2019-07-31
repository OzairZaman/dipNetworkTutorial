using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncTransform : NetworkBehaviour
{
    #region Varianbles
    // speed of lerping roatiion and position
    public float lerprate = 15;

    //thresholds for when to send command
    public float positionThreshold = 0.5f;
    public float rotaitonThreshold = 5.0f;

    //records the previous postion & rotation that was sent to the server
    private Vector3 lastPostion;
    private Quaternion lastRotatoin;

    //vars to be synced across network
    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private Quaternion syncRotaion;

    //obtain rigidbody
    private Rigidbody rigid;

    #endregion

    #region Methods
    void LerpPosition()
    {
        //if current player is not the local player
        if (!isLocalPlayer)
        {
            //lerp postion for all other connected clients
            rigid.position = Vector3.Lerp(rigid.position, syncPosition, lerprate * Time.deltaTime);
        }
    }

    void LerpRotation()
    {
        //if current player is not the local player
        if (!isLocalPlayer)
        {
            rigid.rotation = Quaternion.Lerp(rigid.rotation, syncRotaion, lerprate * Time.deltaTime);
        }
    }

    [Command]
    void CmdSendPostionToServer(Vector3 _position)
    {
        syncPosition = _position;
    }
        
    [Command]
    void CmdSendRotationToServer(Quaternion _rotation)
    {
        syncRotaion = _rotation;
    }

    [ClientCallback] void TransmitPosition()
    {
        if (isLocalPlayer && Vector3.Distance(rigid.position, lastPostion) > positionThreshold)
        {
            CmdSendPostionToServer(rigid.position);
        }
    }

    [ClientCallback] void TransmitRotaion()
    {
        if (isLocalPlayer && Quaternion.Angle(rigid.rotation, lastRotatoin) > rotaitonThreshold)
        {
            CmdSendRotationToServer(rigid.rotation);
        }
    }
    #endregion

    #region Unity
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TransmitPosition();
        LerpPosition();

        TransmitRotaion();
        LerpRotation();
    }
    #endregion

}
