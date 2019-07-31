using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Game Data Collection")]
public class GameData
{
    // we need to convert a class to XML - serialization
    // set this up to be a collection

    //stuff we want to store
    public Vector3 postion; //3 floats
    public Quaternion roattion; // for floats float + w (time)

    // special cases for arrays
    // need to attribute these
    [XmlArray("Dialogue")]
    [XmlArrayItem("Text")]
    public string[] dialogue;


}
