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
        private List<string> plyGameObjects = new List<string> {"Origin", "Wrapper/Origin" };
        private bool leftStickPress;
        private bool isFinished;

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

                if (!isFinished && leftStickPress)
                {
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "GameCore" when Settings.instance.EnableSnapTurn && Settings.instance.EnableInSongs:
                            ScoreSubmission.ProlongedDisableSubmission("SnapTurn");
                            break;
                        default:
                            return;
                    }
                    for (int i = 0; i < plyGameObjects.Count; i++)
                    {
                        currentPly = GameObject.Find(plyGameObjects.ToArray()[i]);
                        if (currentPly != null)
                        {
                            int spinDir = leftStick < 0 ? -15 : 15;
                            currentPly.transform.Rotate(0, spinDir, 0, Space.World);
                        }
                    }
                }
                isFinished = leftStickPress;

            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}