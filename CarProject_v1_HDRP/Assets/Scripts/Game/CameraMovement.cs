  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public Transform[] objectToFollow;
	protected Vector3 offset;
	public string CameraOptionsButton = "n";
	protected int pressCounter = 0;
	
	
		
	public float followSpeed = 10;
	public float lookSpeed = 10;

	void Start() {
		offset.Set(0,1,6);
	}

	public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow[MapCarChoose.getCarNum].position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{
		Vector3 _targetPos = objectToFollow[MapCarChoose.getCarNum].position + 
							 objectToFollow[MapCarChoose.getCarNum].forward * offset.z + 
							 objectToFollow[MapCarChoose.getCarNum].right * offset.x + 
							 objectToFollow[MapCarChoose.getCarNum].up * offset.y;
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
	}
	 public void Update()
	{

	if (Input.GetKeyDown(CameraOptionsButton))
        {
			if(pressCounter == 0){
			offset.Set(0,1,10);
			pressCounter++;
			}
			else if(pressCounter == 1){
			offset.Set(0,1,2);
			pressCounter++;
			}
			else if(pressCounter == 2){
			offset.Set(0,1,6);
			pressCounter = 0;
			}
		
		}
	}

	private void FixedUpdate()
	{
	

		LookAtTarget();
		MoveToTarget();
	}
}