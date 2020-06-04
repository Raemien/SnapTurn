using UnityEngine;
using UnityEngine.XR;
using System;

namespace SnapTurn
{
    public class TurnManager : MonoBehaviour
    {
        //private InputDevice leftController;
        //private InputDevice rightController;
        private GameObject currentPly;
        private bool isFinished;

        public void InitSnapTurnScene()
        {
            //this.leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            //this.rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        private void Update()
        {
            try
            {
                float leftStick = Input.GetAxis("HorizontalLeftHand");

                if (!isFinished && GameObject.Find("Wrapper/Origin") != null && Math.Abs(leftStick) > 0.3)
                {
                    currentPly = GameObject.Find("Wrapper/Origin");
                    int spinDir = leftStick < 0 ? -15 : 15;
                    currentPly.transform.Rotate(0, spinDir, 0, Space.World);
                }

                isFinished = Math.Abs(leftStick) > 0.5 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}