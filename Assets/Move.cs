using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public enum Mass
{
    Mercury,
    Venus,
    Earth,
    Mars,
    Jupiter,
    Saturn,
    Uranus,
    Neptune
}

public class Move : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject stringObj;
    private Mass Planet;
    private float masses;
    [SerializeField]HingeJoint joint;
    [SerializeField]Rigidbody rb;
    Vector3 anchor;
    public float speedH = 2.0f;
    private float yaw = 0.0f;
    private bool RotatePlane;


    public Slider AngleSlide;
    public TMP_Text sliderAngleText;

    public Slider HeightSlide;
    public TMP_Text sliderHeightText;
    
    public Slider DragSlide;
    public TMP_Text sliderDragText;

    public TMP_Dropdown planetDroupDown;
    public TMP_Text planetNamesText;

    public Toggle rotateToggle;
    void Start()
    {
        ChangeMass(Planet);
        PopulateList();
    }

    private void PopulateList()
    {
        string[] planetNamesData = Enum.GetNames(typeof(Mass));
        List<string> planetName= new List<string>(planetNamesData);
        planetDroupDown.AddOptions(planetName);
    }
    public void RotatePlaneActive()
    {
        RotatePlane = !RotatePlane;
        if (!RotatePlane)
        {
            cameraObj.transform.eulerAngles = new Vector3(0, 0, 0.0f);
        }
    }
    public void ChangeMasses()
    {
        Planet = (Mass)planetDroupDown.value;
        ChangeMass(Planet);
    }
    private void ChangeMass(Mass planet)
    {
        print(planet);
        if(planet == Mass.Mercury)
        {
            masses =0.0553f;
        }
        else if(planet == Mass.Venus) 
        {
            masses = 0.815f;
        }
        else if (planet == Mass. Earth)
        {
            masses = 1f;
        }
        else if (planet == Mass.Mars)
        {
            masses = 0.107f;
        }
        else if (planet == Mass.Jupiter)
        {
            masses = 317.8f;
        }
        else if (planet == Mass.Saturn)
        {
            masses = 95.2f;
        }
        else if (planet == Mass.Uranus)
        {
            masses = 14.5f;
        }
        else if (planet == Mass.Neptune)
        {
            masses = 17.1f;
        }
        rb.mass = masses;

    }

    // Update is called once per frame
    void Update()
    {
        if (RotatePlane)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
        }

    }

    public void ActivateGravity()
    {
        rb.useGravity = !rb.useGravity;
    }

    public void ChangeDrag()
    {
        rb.drag = DragSlide.value;
        sliderDragText.text = "Drag: "+ DragSlide.value.ToString("0.0");
    }

    public void HeightChange()
    {
        AngleChange();
        stringObj.transform.localScale = new Vector3(stringObj.transform.localScale.x, HeightSlide.value/10, stringObj.transform.localScale.z);
        anchor = joint.anchor;
        joint.anchor = anchor;
        sliderHeightText.text = "Height: " + HeightSlide.value.ToString("00");
    }

    public void AngleChange()
    {
        stringObj.transform.localRotation = Quaternion.Euler(AngleSlide.value, 0, 0);
        sliderAngleText.text = "Angle: " + AngleSlide.value.ToString("00"+"°");
    }

    private void FixedUpdate()
    {
        if (!RotatePlane) return;
        cameraObj.transform.eulerAngles = new Vector3(0, yaw, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(anchor, .01f);
    }
}
