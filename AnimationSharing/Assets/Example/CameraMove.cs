using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 100f;

    private float mouseX = 0f;
    private float mouseY = 0f;

    void Start()
    {
        // 锁定鼠标到屏幕中央
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        
        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        mouseX += rotationX;
        mouseY -= rotationY;
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        float centerX = Screen.width * 0.5f;
        float centerY = Screen.height * 0.5f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
