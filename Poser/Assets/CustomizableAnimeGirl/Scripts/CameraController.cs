/*
マウス操作でカメラを動かすスクリプト
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CustomizableAnimeGirl
{
    public class CameraController : MonoBehaviour
    {
        private Vector3 targetPos;
        public float cameraMoveSpeed = 0.05f;
        public float cameraRotateSpeed = 200f;
        public float cameraZoomSpeed = 2.0f;

        // Start is called before the first frame update

        void Start()
        {
            targetPos = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            // GUI上の場合はスキップ
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // WASDでカメラ移動
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.up * cameraMoveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += transform.up * -cameraMoveSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * cameraMoveSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += transform.right * -cameraMoveSpeed;
            }

            // 右ドラッグでカメラ回転
            if (Input.GetMouseButton(1))
            {
                float mouseInputX = Input.GetAxis("Mouse X");
                float mouseInputY = Input.GetAxis("Mouse Y");
                transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * cameraRotateSpeed);
                transform.RotateAround(targetPos, transform.right, mouseInputY * Time.deltaTime * -cameraRotateSpeed);
            }

            // マウスホイールでカメラ拡大縮小
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.position += transform.forward * scroll * cameraZoomSpeed;

            // ホイールクリックでカメラ移動
            if (Input.GetMouseButton(2))
            {
                float mouseInputX = Input.GetAxis("Mouse X");
                float mouseInputY = Input.GetAxis("Mouse Y");
                transform.position -= transform.right * mouseInputX * cameraMoveSpeed;
                transform.position -= transform.up * mouseInputY * cameraMoveSpeed;
            }
        }
    }
}