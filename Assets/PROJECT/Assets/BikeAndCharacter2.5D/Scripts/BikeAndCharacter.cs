using Kamgam.BikeAndCharacter25D.Helpers;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Kamgam.BikeAndCharacter25D
{
    public class BikeAndCharacter : MonoBehaviour
    {
        public Bike Bike;
        public Character Character;

        [System.NonSerialized]
        public bool HandleUserInput = true;

        [System.NonSerialized]
        public IBikeTouchInput TouchInput;

        public void Update()
        {
            if (!HandleUserInput)
                return;

            // If TouchInput is configured then touch input takes precedence over key input
            if (TouchInput != null)
            {
                // Touch Input
                Bike.IsBraking = TouchInput.IsBrakePressed();
                Bike.IsSpeedingUp = TouchInput.IsSpeedUpPressed();
                Bike.IsRotatingCW = TouchInput.IsRotateCWPressed();
                Bike.IsRotatingCCW = TouchInput.IsRotateCCWPressed();

                if (TouchInput.WasToggleConnectionPressed())
                {
                    if (Character.IsConnectedToBike)
                        DisconnectCharacterFromBike(Bike, Character, true);
                    else
                        ConnectCharacterToBike(Bike, Character);
                }
            }
            else
            {
#if ENABLE_INPUT_SYSTEM
                // Keyboard or Controller Input (New Input System)
                Bike.IsBraking = ((Keyboard.current != null && (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)) || (Gamepad.current != null && Gamepad.current.leftTrigger.isPressed));
                Bike.IsSpeedingUp = ((Keyboard.current != null && (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)) || (Gamepad.current != null && Gamepad.current.rightTrigger.isPressed));
                Bike.IsRotatingCW = ((Keyboard.current != null && (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)) || (Gamepad.current != null && Gamepad.current.leftStick.right.IsActuated(0.1f)));
                Bike.IsRotatingCCW = ((Keyboard.current != null && (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)) || (Gamepad.current != null && Gamepad.current.leftStick.left.IsActuated(0.1f)));
#else
                // Keyboard Input (Old Input System)
                Bike.IsBraking = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));
                Bike.IsSpeedingUp = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
                Bike.IsRotatingCW = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
                Bike.IsRotatingCCW = (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
#endif

                if (actionPressed())
                {
                    if (Character.IsConnectedToBike)
                        DisconnectCharacterFromBike(Bike, Character, true);
                    else
                        ConnectCharacterToBike(Bike, Character);
                }
            }

            if (Bike.IsRotatingCW)
                Character.TiltForward();
            else if (Bike.IsRotatingCCW)
                Character.TiltBackward();
            else
                Character.StopTilt();
        }

        protected bool actionPressed()
        {
#if ENABLE_INPUT_SYSTEM
            return (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) || (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);
#else
            return Input.GetKeyDown(KeyCode.Space);
#endif
        }

        public void StopAllInput()
        {
            Bike.IsBraking = false;
            Bike.IsSpeedingUp = false;
            Bike.IsRotatingCW = false;
            Bike.IsRotatingCCW = false;

            Character.StopTilt();
        }

        public void DisconnectCharacterFromBike(Bike bike, Character character, bool addImpulse = true)
        {
            // disconnect char if needed
            if (character.IsConnectedToBike)
            {
                character.DisconnectFromBike(bike, addImpulse);
                bike.StopAllInput();
            }
        }

        public void ConnectCharacterToBike(Bike bike, Character character)
        {
            if (!character.IsConnectedToBike)
            {
                character.ConnectToBike(Bike);
                bike.StopAllInput();
            }
        }
    }
}