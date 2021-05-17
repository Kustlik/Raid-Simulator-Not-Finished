using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScreenSwitch : MonoBehaviour
{
    [SerializeField] GameObject freehold;
    [SerializeField] GameObject uldir;
    [SerializeField] GameObject bod;
    [SerializeField] GameObject eP;
    [SerializeField] GameObject nya;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject uldirMenu;
    [SerializeField] GameObject bodMenu;
    [SerializeField] GameObject epMenu;
    [SerializeField] GameObject nyaMenu;

    GameObject activePlane;
    GameObject oldPlane;

    float activePlaneAlphaLevel = 1;
    float oldPlaneAlphaLevel = 0;

    bool doItOnce = true;
    bool isAlphaDownActive = false;
    bool isAlphaUpActive = false;

    float ad = 0;
    float au = 0;

    void Start()
    {
        freehold.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "main_menu2.mp4");
        bod.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "BoD.mp4");
        eP.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "EP.mp4");
        nya.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "Nyalotha.mp4");
        uldir.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "Uldir.mp4");

        freehold.GetComponent<VideoPlayer>().Play();
        bod.GetComponent<VideoPlayer>().Play();
        eP.GetComponent<VideoPlayer>().Play();
        nya.GetComponent<VideoPlayer>().Play();
        uldir.GetComponent<VideoPlayer>().Play();

        activePlane = nya;
    }

    void Update()
    {
        if (mainMenu.activeInHierarchy == true)
        {
            MainMenuCheck();
        }
        if (uldirMenu.activeInHierarchy == true)
        {
            UldirMenuCheck();
        }
        if (bodMenu.activeInHierarchy == true)
        {
            BoDMenuCheck();
        }
        if (epMenu.activeInHierarchy == true)
        {
            EpMenuCheck();
        }
        if (nyaMenu.activeInHierarchy == true)
        {
            NyaMenuCheck();
        }

        AlphaDown();
        AlphaUp();
    }

    void AlphaDown()
    {
        if (isAlphaDownActive == true)
        {
            if(doItOnce == true)
            {
                oldPlaneAlphaLevel = activePlane.GetComponent<VideoPlayer>().targetCameraAlpha;
                doItOnce = false;
            }

            oldPlane.GetComponent<VideoPlayer>().targetCameraAlpha = Mathf.SmoothStep(oldPlaneAlphaLevel, 0, ad);

            ad += 1 * Time.deltaTime;

            if (oldPlane.GetComponent<VideoPlayer>().targetCameraAlpha == 0)
            {
                ad = 0;
                oldPlaneAlphaLevel = 0;
                oldPlane.SetActive(false);
                isAlphaDownActive = false;
                isAlphaUpActive = true;
            }
        }
    }

    void AlphaUp()
    {
        if (isAlphaUpActive == true)
        {
            activePlane.SetActive(true);
            activePlane.GetComponent<VideoPlayer>().targetCameraAlpha = Mathf.SmoothStep(0, 1, au);

            au += 1 * Time.deltaTime;

            if (activePlane.GetComponent<VideoPlayer>().targetCameraAlpha == 1)
            {
                au = 0;
                isAlphaUpActive = false;
            }
        }
    }

    void MainMenuCheck()
    {
        if (activePlane != freehold)
        {
            isAlphaUpActive = false;

            if (oldPlaneAlphaLevel == 0)
            {
                oldPlane = activePlane;
            }

            activePlane = freehold;
            au = 0;

            isAlphaDownActive = true;
        }
    }

    void UldirMenuCheck()
    {
        if (activePlane != uldir)
        {
            isAlphaUpActive = false;

            if (oldPlaneAlphaLevel == 0)
            {
                oldPlane = activePlane;
            }

            activePlane = uldir;
            au = 0;

            isAlphaDownActive = true;
        }
    }

    void BoDMenuCheck()
    {
        if (activePlane != bod)
        {
            isAlphaUpActive = false;

            if (oldPlaneAlphaLevel == 0)
            {
                oldPlane = activePlane;
            }

            activePlane = bod;
            au = 0;

            isAlphaDownActive = true;
        }
    }

    void EpMenuCheck()
    {
        if (activePlane != eP)
        {
            isAlphaUpActive = false;

            if (oldPlaneAlphaLevel == 0)
            {
                oldPlane = activePlane;
            }

            activePlane = eP;
            au = 0;

            isAlphaDownActive = true;
        }
    }

    void NyaMenuCheck()
    {
        if (activePlane != nya)
        {
            isAlphaUpActive = false;

            if (oldPlaneAlphaLevel == 0)
            {
                oldPlane = activePlane;
            }

            activePlane = nya;
            au = 0;

            isAlphaDownActive = true;
        }
    }

}
