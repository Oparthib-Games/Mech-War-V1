using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserScript : MonoBehaviour {

    public float range = 10f;
    public float damage = 5f;

    Ray theRay;
    RaycastHit hitInfo;
    public LayerMask shootableMask;
    LineRenderer gunLine;

	void Awake () {
        gunLine = GetComponent<LineRenderer>();

        theRay.origin = transform.position;
        theRay.direction = transform.forward;

        gunLine.SetPosition(0, transform.position);

        if(Physics.Raycast(theRay, out hitInfo, range, shootableMask))
        {
            hitInfo.transform.gameObject.GetComponent<health_script>().dealDamage(damage);
            gunLine.SetPosition(1, hitInfo.point);
        }
        else
        {
            gunLine.SetPosition(1, theRay.origin + theRay.direction * range);
        }

//============================================== DESTROY ==============================================================
        Destroy(gameObject, 1f);
	}
	
}
