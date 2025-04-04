using UnityEngine;
using UnityEngine.UI;

public class WheelManager : MonoBehaviour
{
    public void ToggleWheel(bool open)
    {
        if (open)
        {
            OpenWheel();
        }
        else
        {
            CloseWheel();
        }
    }
   
    private void OpenWheel()
    {
        WeaponWheelController.Instance.OpenWheel();
    }
    private void CloseWheel()
    {
        WeaponWheelController.Instance.CloseWheel();
    }
}
