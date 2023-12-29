using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>().gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    float hor = 0;
    float vert = 0;
    public float speed =3f;
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensivityHor = 9.0f;
    public float sensivityVert = 9.0f;

    public float minimumVert = -89.0f;
    public float maximumVert = 89.0f;

    private float _rotationX = 0;

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        if (axes == RotationAxes.MouseX)
        {
            cam.transform.Rotate(0, Input.GetAxis("Mouse X") * sensivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX += Input.GetAxis("Mouse Y") * sensivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float rotationY = cam.transform.localEulerAngles.y;
            cam.transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX += Input.GetAxis("Mouse Y") * sensivityVert * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float delta = Input.GetAxis("Mouse X") * sensivityHor * Time.deltaTime;
            float rotationY = cam.transform.localEulerAngles.y + delta;
            cam.transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = (cam.transform.right * hor + cam.transform.forward * vert) * Time.fixedDeltaTime * speed;
        this.transform.position += moveDirection;


    }
}
