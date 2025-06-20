using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 50f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    void Start()
    {
        // ��������� ������ � �������� ��� � ������ ����
        LockCursor();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
            // �������� ������ � �������� ����
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // �������� �� ��� X (������� ����� � ����)
            xRotation -= mouseY;

            // ����������� �������� 
            xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

            // �������� �� ��� Y (������� ����� � ������)
            yRotation += mouseX;

            // ���������� �������� � ������ ����������
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
