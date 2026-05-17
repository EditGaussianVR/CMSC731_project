using UnityEngine;
using UnityEngine.InputSystem;

public class VRVerticalMove : MonoBehaviour
{
    [SerializeField] Transform xrOrigin;
    [SerializeField] InputActionProperty upDownAction;
    [SerializeField] float speed = 2f;

    void OnEnable()  => upDownAction.action.Enable();
    void OnDisable() => upDownAction.action.Disable();

    void Update()
    {
        float v = upDownAction.action.ReadValue<Vector2>().y;
        xrOrigin.position += Vector3.up * v * speed * Time.deltaTime;
    }
}