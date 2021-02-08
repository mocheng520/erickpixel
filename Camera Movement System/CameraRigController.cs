using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraMovementSystem
{
    public class CameraRigController : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraRigTransform;

        [SerializeField]
        [Space]
        [Range(1, 100)]
        private float cameraMovementSpeed;
        [SerializeField]
        [Range(1, 100)]
        private float screenDeadZone;

        public Vector3 mousePositionScreen
        {
            get;
            set;
        }

        private float screenRightSide
        {
            get
            {
                return Screen.width - screenDeadZone;
            }
        }
        private float screenLeftSide
        {
            get
            {
                return screenDeadZone;
            }
        }
        private float screenUpper
        {
            get
            {
                return Screen.height - screenDeadZone;
            }
        }
        private float screenBottom
        {
            get
            {
                return screenDeadZone;
            }
        }
        private bool canProcess
        {
            get
            {
                return mousePositionScreen.sqrMagnitude != 0;
            }
        }

        private void LateUpdate()
        {
            if (canProcess)
            {
                MoveCameraUp();

                MoveCameraDown();

                MoveCameraToLeft();

                MoveCameraToRight();
            }
        }

        private void MoveCameraDown()
        {
            if (mousePositionScreen.y < screenBottom ||
                Keyboard.current.sKey.wasReleasedThisFrame)
                cameraRigTransform.position -= cameraRigTransform.transform.forward * cameraMovementSpeed * Time.deltaTime;
        }

        private void MoveCameraUp()
        {
            if (mousePositionScreen.y >= screenUpper ||
                Keyboard.current.wKey.wasReleasedThisFrame)
                cameraRigTransform.position += cameraRigTransform.transform.forward * cameraMovementSpeed * Time.deltaTime;
        }

        private void MoveCameraToLeft()
        {
            if (mousePositionScreen.x < screenLeftSide ||
                Keyboard.current.aKey.wasReleasedThisFrame)
                cameraRigTransform.position -= cameraRigTransform.transform.right * cameraMovementSpeed * Time.deltaTime;
        }

        private void MoveCameraToRight()
        {
            if (mousePositionScreen.x >= screenRightSide ||
                Keyboard.current.dKey.wasReleasedThisFrame)
                cameraRigTransform.position += cameraRigTransform.transform.right * cameraMovementSpeed * Time.deltaTime;
        }
    }
}