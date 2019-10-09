using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets a Movement Vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets a Camera Rotational Vector
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //Get a force for our thruster
    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    //Gets a Rotational Vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Run Every physics iterations
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform Movement based on the velocity variable
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime,ForceMode.Acceleration);
        }
    }

    //Perform Rotation based on the velocity variable
    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //  Old Rotational Calculation : cam.transform.Rotate(-cameraRotation);
            // New Rotational Calculation
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);


            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }

    }
}

