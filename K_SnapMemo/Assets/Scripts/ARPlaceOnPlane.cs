using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObj;

    GameObject spawnObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCenterObj();
        //PlaceObjByTouch();
    }

    private void PlaceObjByTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

                if (!spawnObj)
                {
                    spawnObj = Instantiate(placeObj, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnObj.transform.position = hitPose.position;
                    spawnObj.transform.rotation = hitPose.rotation;
                }
            }
        }
        
    }

    private void UpdateCenterObj()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3 (0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObj.SetActive(true);
            placeObj.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
    }
}
