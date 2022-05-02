using UnityEngine;

namespace GameJam
{
    public  class DeviceManager : MonoBehaviour
    {
        public static bool GamePadActive()
        {
            bool thereIsAxis = Input.GetAxis("XboxMouseX") != 0 || Input.GetAxis("XboxMouseY") != 0 ||
                               Input.GetAxis("XboxTurn") != 0 || Input.GetAxis("XboxWalk") != 0 ||
                               Input.GetAxis("XboxDodge") != 0 || Input.GetAxis("XboxAttack") != 0 ||
                               Input.GetAxis("XboxJump") != 0;
            return thereIsAxis;
        }
    }
}