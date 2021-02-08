using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace PixelRTS.Utilities
{
    public static class Utility
    {
        public static Vector3 MousePosition
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return Mouse.current.position.ReadValue();
#else
                return Input.mousePosition;
#endif
            }
        }

        public static Vector3 GetMousePositionIn3DWorld()
        {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(MousePosition);
            
            if (Physics.Raycast(ray, out var hit, cam.farClipPlane * 2))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(int targetLayer)
        {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(MousePosition);

            if (Physics.Raycast(ray, out var hit, cam.farClipPlane * 2, targetLayer))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(Vector3 mousePositionScreen)
        {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(mousePositionScreen);

            if (Physics.Raycast(ray, out var hit, cam.farClipPlane * 2))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(Vector3 mousePositionScreen, int targetLayer)
        {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(mousePositionScreen);

            if (Physics.Raycast(ray, out var hit, cam.farClipPlane * 2, targetLayer))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(Camera camera)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);

            if (Physics.Raycast(ray, out var hit, camera.farClipPlane * 2))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(Camera camera, int targetLayer)
        {
            Ray ray = camera.ScreenPointToRay(MousePosition);

            if (Physics.Raycast(ray, out var hit, camera.farClipPlane * 2, targetLayer))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(Vector3 mousePositionScreen, Camera camera)
        {
            Ray ray = camera.ScreenPointToRay(mousePositionScreen);

            if (Physics.Raycast(ray, out var hit, camera.farClipPlane * 2))
            {
                Vector3 worldPoint = hit.point;

                // Changes are need?

                return worldPoint;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMousePositionIn3DWorld(Vector3 mousePositionScreen, Camera camera, int targetLayer)
        {
            Ray ray = camera.ScreenPointToRay(mousePositionScreen);

            if (Physics.Raycast(ray, out var hit, camera.farClipPlane * 2, targetLayer))
            {
                Vector3 worldPoint = hit.point;

                // Changes are needed?

                return worldPoint;
            }

            return Vector3.zero;
        }
    }
}