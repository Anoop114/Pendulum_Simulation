using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Move : MonoBehaviour
{
    public GameObject cameraObj;
    public GameObject stringObj;
    public float yAngle;
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
    [Range(1,10)]public int Height;
    [Range(0,90)]public int Angle;
    [Range(0,10)]public int Drag;
    public Mass Planet;
    private Mass tempPlanet;
    private int tempHeight;
    private int tempAngle;
    private int tempDrag;
    private float masses;
    [SerializeField]HingeJoint joint;
    [SerializeField]Rigidbody rb;
    Vector3 anchor;
    [SerializeField] bool useGravity;


    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    public bool RotatePlane;


    public Slider AngleSlide;
    public TMP_Text sliderAngleText;

    public Slider HeightSlide;
    public TMP_Text sliderHeightText;
    
    public Slider DragSlide;
    public TMP_Text sliderDragText;

    public TMP_Dropdown planetDroupDown;
    public TMP_Text planetNamesText;
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //joint = GetComponent<HingeJoint>();
        tempHeight = Height;
        tempAngle = Angle;
        tempPlanet = Planet;
        tempDrag = Drag;
        ChangeMass(Planet);
        PopulateList();
    }

    private void PopulateList()
    {
        string[] planetNamesData = Enum.GetNames(typeof(Mass));
        List<string> planetName= new List<string>(planetNamesData);
        planetDroupDown.AddOptions(planetName);
    }
    public void ChangeMasses()
    {
        tempPlanet = (Mass)planetDroupDown.value;
        ChangeMass(tempPlanet);
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
        //ChangeDrag();
        //AngleChange();
        //HeightChange();
        if (useGravity)
        {
            useGravity = false;
            rb.useGravity = true;
        }
        if (RotatePlane)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
        }
    }

    public void ChangeDrag()
    {
        rb.drag = DragSlide.value;
        sliderDragText.text = "Drag: "+ DragSlide.value.ToString("0.0");
        //if (tempDrag != Drag)
        //{
        //    rb.drag = Drag / 10f;
        //    tempDrag = Drag;
        //}
    }

    public void HeightChange()
    {
        stringObj.transform.localScale = new Vector3(stringObj.transform.localScale.x, HeightSlide.value, stringObj.transform.localScale.z);
        anchor = joint.anchor;
        joint.anchor = anchor;
        sliderHeightText.text = "Height: " + HeightSlide.value.ToString("00");

        //if (tempHeight != Height)
        //{
        //    stringObj.transform.localScale = new Vector3(stringObj.transform.localScale.x, tempHeight, stringObj.transform.localScale.z);
        //    anchor = joint.anchor;
        //    joint.anchor = anchor;
        //    tempHeight = Height;
        //}
    }

    public void AngleChange()
    {
        
        stringObj.transform.localRotation = Quaternion.Euler(AngleSlide.value, 0, 0);
        sliderAngleText.text = "Angle: " + AngleSlide.value.ToString("00"+"°");
        //if (tempAngle != Angle) 
        //{
        //    stringObj.transform.localRotation = Quaternion.Euler(Angle, 0, 0);

        //    tempAngle = Angle;
        //}
    }

    private void FixedUpdate()
    {
        if (!RotatePlane)
        {
            cameraObj.transform.eulerAngles = new Vector3(0, 0, 0.0f);
        }
        cameraObj.transform.eulerAngles = new Vector3(0, yaw, 0.0f);
    }
}
