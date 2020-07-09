using BS_Utils.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace SnapTurn
{

    public class TurnManager : MonoBehaviour
    {
        private InputDevice leftController;
        private InputDevice rightController;
        private GameObject currentObj;
        private List<string> plyGameObjects = new List<string> { "Origin", "Wrapper/Origin", "Wrapper/PauseMenu/MenuControllers" };
        private bool leftStickPress;
        private bool isFinished;
        private int turnStep;
        private void Update()
        {
            try
            {
                leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

                leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftStickPress);
                float leftStick = Input.GetAxis("HorizontalLeftHand");
                string curscene = SceneManager.GetActiveScene().name;
                var config = Settings.instance;

                turnStep = config.RotationStep;

                this.enabled = (config.EnableTurning && !(curscene == "GameCore" && !config.EnableInSongs));

                if (turnStep > 5 && config.SmoothTurn)
                {
                    turnStep = 5;
                    config.RotationStep = 5;
                }

                if (!isFinished && leftStickPress)
                {
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "GameCore" when config.EnableTurning && config.EnableInSongs:
                            ScoreSubmission.ProlongedDisableSubmission("SnapTurn");
                            break;
                        default:
                            break;
                    }

                    int spinDir = leftStick < 0 ? (0 - turnStep) : turnStep;
                    this.transform.Rotate(0, spinDir, 0, Space.World);
                    SetSceneRotations();
                }
                isFinished = (leftStickPress && !config.SmoothTurn);
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