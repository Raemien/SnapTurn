//using UnityEngine;
//using UnityEngine.XR;

//namespace SnapTurn
//{
//    /// <summary>
//    /// This class manages the input system, most importantly helping to gate button presses
//    /// on the controller, basically debouncing buttons.
//    /// </summary>
//    public class InputManager : MonoBehaviour
//    {
//        private InputDevice leftController;
//        private InputDevice rightController;
//        private bool leftTriggerCanClick;
//        private bool leftTriggerDown;
//        private bool rightTriggerCanClick;
//        private bool rightTriggerDown;
//        private bool isPolling;

//        #region Button Polling Methods

//        public bool GetLeftTriggerDown()
//        {
//            return leftTriggerDown;
//        }

//        public bool GetLeftTriggerClicked()
//        {
//            bool returnValue = false;
//            if (leftTriggerCanClick && leftTriggerDown)
//            {
//                returnValue = true;
//                leftTriggerCanClick = false;
//            }
//            return returnValue;
//        }

//        public bool GetRightTriggerDown()
//        {
//            return rightTriggerDown;
//        }

//        public bool GetRightTriggerClicked()
//        {
//            bool returnValue = false;
//            if (rightTriggerCanClick && rightTriggerDown)
//            {
//                returnValue = true;
//                rightTriggerCanClick = false;
//            }

//            return returnValue;
//        }

//        #endregion

//        #region MonoBehavior Methods

//        public void BeginGameCoreScene()
//        {
//            this.leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
//            this.rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

//            leftTriggerCanClick = true;
//            rightTriggerCanClick = true;
//            isPolling = true;
//        }

//        internal void DisableInput()
//        {
//            isPolling = false;
//        }

//        private void Update()
//        {

//            if (!isPolling) return;
//            this.leftController.TryGetFeatureValue(CommonUsages.menuButton, out leftTriggerDown);
//            this.rightController.TryGetFeatureValue(CommonUsages.menuButton, out rightTriggerDown);
//        }

//        #endregion
//    }
//}