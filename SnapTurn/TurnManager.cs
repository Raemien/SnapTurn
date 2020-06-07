using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System;
using System.Collections.Generic;
using BS_Utils.Gameplay;

namespace SnapTurn
{

    public class TurnManager : MonoBehaviour
    {
        private InputDevice leftController;
        private InputDevice rightController;
        private GameObject currentPly;
        private List<string> plyGameObjects = new List<string> {"Origin", "Wrapper/Origin", "Wrapper/PauseMenu/MenuControllers"};
        private bool leftStickPress;
        private bool isFinished;
        private int turnStep;

        public void InitSnapTurnScene()
        {
            // This is currently reduntant, although I intend to define variables here in future to improve performance.
        }


        private void Update()
        {
            try
            {
                leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

                leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out leftStickPress);
                float leftStick = Input.GetAxis("HorizontalLeftHand");

                turnStep = Settings.instance.RotationStep;

                if (turnStep > 5 && Settings.instance.SmoothTurn)
                {
                    turnStep = 5;
                    Settings.instance.RotationStep = 5;
                }

                if (!isFinished && leftStickPress)
                {
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "GameCore" when Settings.instance.EnableSnapTurn && Settings.instance.EnableInSongs:
                            ScoreSubmission.ProlongedDisableSubmission("SnapTurn");
                            break;
                        default:
                            break;
                    }

                    int spinDir = leftStick < 0 ? (0 - turnStep) : turnStep;
                    this.transform.Rotate(0, spinDir, 0, Space.World);

                    for (int i = 0; i < plyGameObjects.Count; i++)
                    {
                        currentPly = GameObject.Find(plyGameObjects.ToArray()[i]);
                        if (currentPly != null)
                        {
                            currentPly.transform.rotation = this.transform.rotation;
                        }
                    }
                }
                isFinished = (leftStickPress && !Settings.instance.SmoothTurn);

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}