using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [Header("Camera Bob")]
    [SerializeField] private float bobAmplitude = 0.2f;
    [SerializeField] private float bobFrequency = 6f;
    [SerializeField] private float bobReturnSpeed = 8f;
    private Transform cameraBobTarget;
    private Vector3 _baseLocalPos;
    private float _bobTimer;

    void Start()
    {
        cameraBobTarget = GetComponent<Camera>().transform;
        _baseLocalPos = cameraBobTarget.localPosition;
    }

    void LateUpdate()
    {
        if (InputManager.Instance == null) return;
        // if (UIController.Instance != null && UIController.Instance.IsInventoryShown)
        // {
        //     _bobTimer = 0f;
        //     cameraBobTarget.localPosition = Vector3.Lerp(
        //         cameraBobTarget.localPosition,
        //         _baseLocalPos,
        //         bobReturnSpeed * Time.deltaTime
        //     );
        //     return;
        // }

        Vector2 moveInput = InputManager.Instance.MoveInput;
        float moveAmount = Mathf.Clamp01(moveInput.magnitude);

        if (moveAmount > 0.01f)
        {
            _bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(_bobTimer) * bobAmplitude * moveAmount;
            Vector3 targetPos = _baseLocalPos + new Vector3(0f, bobOffset, 0f);
            cameraBobTarget.localPosition = targetPos;
        }
        else
        {
            _bobTimer = 0f;
            cameraBobTarget.localPosition = Vector3.Lerp(
                cameraBobTarget.localPosition,
                _baseLocalPos,
                bobReturnSpeed * Time.deltaTime
            );
        }
    }

}