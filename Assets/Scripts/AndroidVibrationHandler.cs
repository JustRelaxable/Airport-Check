using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidVibrationHandler
{

    AndroidJavaObject vibrationHandlerObject; //= new AndroidJavaObject("com.Rc2.AirportCheck.VibrationHandler");
    

    public AndroidVibrationHandler()
    {
        
    }

    public void CreateVibration()
    {
        vibrationHandlerObject = new AndroidJavaObject("com.Rc2.AirportCheck.VibrationHandler");
        vibrationHandlerObject.Call("vibrate");
    }
}
