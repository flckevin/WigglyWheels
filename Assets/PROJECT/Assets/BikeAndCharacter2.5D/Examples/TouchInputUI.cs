using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kamgam.BikeAndCharacter25D
{
    public class TouchInputUI : MonoBehaviour, IBikeTouchInput
    {
        public GameObject SpeedUpButton;
        public GameObject BrakeButton;
        public GameObject RotateCWButton;
        public GameObject RotateCCWButton;
        public GameObject ToggleConnectionButton;

        bool isSpeedUpBtnPressed;
        bool isBrakeBtnPressed;
        bool isRotateCWBtnPressed;
        bool isRotateCCWBtnPressed;
        bool wasToggleConnectionBtnPressed;

        PointerEventData pointerEventData;
        GraphicRaycaster raycaster;
        EventSystem eventSystem;
        List<RaycastResult> raycastResults;
        int frameCounter;

        void Start()
        {
            raycaster = GetComponent<GraphicRaycaster>();
            eventSystem = EventSystem.current;
            pointerEventData = new PointerEventData(eventSystem);
            raycastResults = new List<RaycastResult>();
        }

        void OnEnable()
        {
            frameCounter = 0;
        }

        // Called before update
        public void OnToggleButtonPressed()
        {
            wasToggleConnectionBtnPressed = true;
        }

        // We do check whether or not a finge/mouse is pressing a button continuously every N frames.
        // This is done to simplify the Input code of the bike as with this we can check keyDown and touchDown
        // events in the same way. This uses the OLD input system.

        void Update()
        {
            // Do raycasts every nth frame if the ui is logically shown
            if (++frameCounter % 5 == 0)
            {
                isSpeedUpBtnPressed = false;
                isBrakeBtnPressed = false;
                isRotateCWBtnPressed = false;
                isRotateCCWBtnPressed = false;

                if (Input.touchSupported)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        var touch = Input.GetTouch(i);
                        doRaycast(touch.position);
                    }
                }
                else if (Input.mousePresent && Input.GetMouseButton(0))
                {
                    doRaycast(Input.mousePosition);
                }
            }
        }

        // called after update
        void LateUpdate()
        {
            // wasToggleConnectionBtnPressed will be true in the NEXT frame.
            wasToggleConnectionBtnPressed = false;
        }

        protected void doRaycast(Vector2 position)
        {
            raycastResults.Clear();
            pointerEventData.position = position;
            raycaster.Raycast(pointerEventData, raycastResults);
            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject == SpeedUpButton)
                {
                    isSpeedUpBtnPressed = true;
                    break;
                }
                if (result.gameObject == BrakeButton)
                {
                    isBrakeBtnPressed = true;
                    break;
                }
                if (result.gameObject == RotateCWButton)
                {
                    isRotateCWBtnPressed = true;
                    break;
                }
                if (result.gameObject == RotateCCWButton)
                {
                    isRotateCCWBtnPressed = true;
                    break;
                }
            }
        }

        bool IBikeTouchInput.IsSpeedUpPressed()
        {
            return isSpeedUpBtnPressed;
        }

        bool IBikeTouchInput.IsBrakePressed()
        {
            return isBrakeBtnPressed;
        }

        public bool IsRotateCWPressed()
        {
            return isRotateCWBtnPressed;
        }

        public bool IsRotateCCWPressed()
        {
            return isRotateCCWBtnPressed;
        }

        public bool WasToggleConnectionPressed()
        {
            return wasToggleConnectionBtnPressed;
        }
    }
}