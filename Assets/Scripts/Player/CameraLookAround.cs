using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
public enum RotationAxes {MouseXAndY = 0, MouseX = 1, MouseY = 2}

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;
    public float maximumVert = 45.0f;
    public float minimumVert = -45.0f;
    private float _rotationX = 0;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (InputManager.Instance == null) return;
        Vector2 input = InputManager.Instance.MouseInput;

        if (axes == RotationAxes.MouseX){
            // horizontal
            transform.Rotate(0, input.x * sensitivityHor, 0);
        } else if (axes == RotationAxes.MouseY){
            // vertical
            _rotationX -= input.y * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        // } else {
        //     // both horizontal and vertical
        //     _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        //     _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        //     float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        //     float rotationY = transform.localEulerAngles.y + delta;
        //     transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        // }
    }
}
