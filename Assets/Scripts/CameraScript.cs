using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    private bool camAvailable;
    private WebCamTexture userCamera;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;

    void Start()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.LogWarning("User does not have a camera");
            camAvailable = false;
        }
        else
        {
            for (int i = 0; i < devices.Length; i++)
            {
                // TODO: Add this if statement back before building
                //if (!devices[i].isFrontFacing)
                //{
                    userCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                //}
            }

            if (userCamera == null)
            {
                camAvailable = false;
                Debug.LogWarning("Unable to find back camera");
            }
            else
            {
                userCamera.Play();
                background.texture = userCamera;
                camAvailable = true;
            }
        }
    }

    void Update()
    {
        if (camAvailable)
        {
            float ratio = (float)userCamera.width / (float)userCamera.height;
            fit.aspectRatio = ratio;

            float scaleY = userCamera.videoVerticallyMirrored ? -1f : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -userCamera.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }
}
