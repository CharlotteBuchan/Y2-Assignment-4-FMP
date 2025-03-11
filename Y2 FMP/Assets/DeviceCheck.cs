using UnityEngine;
public class DifferenceExample : MonoBehaviour
{
    void Start()
    {
        // Example built with regual if-statement
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.LinuxEditor)
        {
            Debug.Log("You are running this code in the editor.");
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("This is Android platform.");
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Debug.Log("This is iOS platform.");
        }
    }

    void New()
    {

    #if UNITY_EDITOR
        Debug.Log("UNITY EDITOR");
    #endif

    #if UNITY_IOS
              Debug.Log("iOS");
    #endif

    #if UNITY_STANDALONE_OSX
            Debug.Log("Standalone OSX");
    #endif

    #if UNITY_STANDALONE_WIN
            Debug.Log("Standalone Windows");
    #endif


    }
}