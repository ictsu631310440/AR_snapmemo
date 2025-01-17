using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[ RequireComponent(typeof(ARRaycastHit))]
public class NewARPlaceObj : MonoBehaviour
{

    public GameObject gameObjectToInsantiate;

    private GameObject SpawnedObj;
    private ARRaycastManager aRRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount>0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (SpawnedObj==null)
            {
                SpawnedObj = Instantiate(gameObjectToInsantiate, hitPose.position, hitPose.rotation);
            }
            else
            {
                SpawnedObj.transform.position = hitPose.position;
            }
        }
        
    }
}
