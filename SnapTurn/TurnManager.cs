using BS_Utils.Gameplay;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace SnapTurn
{

    public class TurnManager : MonoBehaviour
    {
        private InputDevice mainController;
        private Vector2 hasJoystick;
        private GameObject currentObj;
        private List<string> plyGameObjects = new List<string> { "Origin", "Wrapper/Origin", "Wrapper/PauseMenu/MenuControllers" };
        private bool mainStickPress;
        private bool isFinished;
        private int turnStep;
        private void Update()
        {
            try
            {
                var config = Settings.instance;

                mainController = config.SelectedController == "Left" ? InputDevices.GetDeviceAtXRNode(XRNode.LeftHand) : InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

                mainController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out mainStickPress);
                float mainStick = Input.GetAxis("Horizontal"+config.SelectedController+"Hand");
                string curscene = SceneManager.GetActiveScene().name;

                turnStep = config.RotationStep;

                this.enabled = (config.EnableTurning && !(curscene == "GameCore" && !config.EnableInSongs));

                if (turnStep > 5 && config.SmoothTurn)
                {
                    turnStep = 5;
                    config.RotationStep = 5;
                }

                mainController.TryGetFeatureValue(CommonUsages.secondary2DAxis, out hasJoystick);

                string conManu = mainController.manufacturer;
                if (hasJoystick != Vector2.zero || (conManu == "Oculus" || (conManu == "Valve" && config.InputType == "Default")) || config.InputType == "Touch")
                {
                    mainStickPress = Math.Abs(mainStick) > 0.1; // Devices with joysticks shouldn't require a full press, unless overridden in settings.
                }

                if (!isFinished && mainStickPress)
                {
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "GameCore" when config.EnableTurning && config.EnableInSongs:
                            ScoreSubmission.ProlongedDisableSubmission("SnapTurn");
                            break;
                        default:
                            break;
                    }

                    int spinDir = mainStick < 0 ? (0 - turnStep) : turnStep;
                    this.transform.Rotate(0, spinDir, 0, Space.World);
                    SetSceneRotations();
                }
                isFinished = (mainStickPress && !config.SmoothTurn);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void SetSceneRotations()
        {
            for (int i = 0; i < plyGameObjects.Count; i++)
            {
                currentObj = GameObject.Find(plyGameObjects.ToArray()[i]);
                if (currentObj != null)
                {
                    currentObj.transform.rotation = this.transform.rotation;
                }
            }
        }

    }
}