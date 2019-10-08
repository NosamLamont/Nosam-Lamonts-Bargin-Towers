using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    private PlayerStats myStats = new PlayerStats();
    public Camera MainCamera;
    public Vector3 moveUpVector = new Vector3(0f, 0.1f, 0.1f);
    public Vector3 moveRightVector = new Vector3(0.1f, 0f, 0f);
    public Vector3 zoomVector = new Vector3(0f, 0f, 0f);
    public Transform mouseTransform;
    public int MouseBuffer = 0;
    public RaycastHit CastHit;
    public float minFOV = 15f;
    public float maxFOV = 90f;
    public float sensitivity = 10f;
    public List<GameObject> SelectedObjects = new List<GameObject>();
    private Vector2 orgBoxPos;
    private Vector2 endBoxPos;
    public Texture selectTexture;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        mouseTransform = MainCamera.transform;
        myStats.Gold = 100;
        myStats.Lives = 20;
    }

    void Update()
    {
        if(Input.GetKeyDown((KeyCode)ControlScheme.Antiract))
        {
            if(SelectedObjects.Count != 0)
            {
                Debug.Log("Antiract: Cleared SelectedObjects");
                SelectedObjects.Clear();
            }
        }              
        
        if (Input.GetKey((KeyCode)ControlScheme.MoveCameraUp) || Input.mousePosition.y >= Screen.height - MouseBuffer)
        {
            MainCamera.transform.Translate(moveUpVector);
        }

        if (Input.GetKey((KeyCode)ControlScheme.MoveCameraDown) || Input.mousePosition.y <=  MouseBuffer)
        {
            MainCamera.transform.Translate(-moveUpVector);
        }

        if (Input.GetKey((KeyCode)ControlScheme.MoveCameraRight) || Input.mousePosition.x >= Screen.width - MouseBuffer)
        {
            MainCamera.transform.Translate(moveRightVector);
        }

        if (Input.GetKey((KeyCode)ControlScheme.MoveCameraLeft) || Input.mousePosition.x <= MouseBuffer)
        {
            MainCamera.transform.Translate(-moveRightVector);
        }

        if (Input.GetKeyDown((KeyCode)ControlScheme.LeftClick))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out CastHit, 100.0f))
            {
                if(CastHit.transform.gameObject.GetComponent<Unit>() != null)
                {
                    Debug.Log("Object Hit: " + CastHit.transform.gameObject.name);
                    //Look up what a fucking predicate is?!?!
                    if(!SelectedObjects.Exists(o=>o == CastHit.transform.gameObject))
                        SelectedObjects.Add(CastHit.transform.gameObject);
                }
                //Temporary Terrain Class for testing the floor.
                if(CastHit.transform.gameObject.GetComponent<Terrain>() != null)
                {
                    Debug.Log("isFloor");
                    if(SelectedObjects.Count != 0)
                        SelectedObjects.Clear();

                }
            }
        }

        if(Input.GetKeyDown((KeyCode)ControlScheme.RightClick))
        {
            if(SelectedObjects.Count != 0)
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out CastHit, 100.0f))
                {
                    foreach(var unit in SelectedObjects)
                    {
                        unit.gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                        unit.gameObject.transform.position = new Vector3(CastHit.point.x, CastHit.point.y + (unit.GetComponent<BoxCollider>().size.y / 2), CastHit.point.z);
                    }
                }
            }
        }
                
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFOV, maxFOV);
        Camera.main.fieldOfView = fov;


    }
 }
