
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Transform is used for position, in this case for the traget(Player).
    public Transform target;
    public float smoodspeed = 0.125f;
    //this is for correct positioning of the camera and not be inside our character.
    public Vector3 offset;

    //Used late update for camera and player movement do not compete for ho is going first and also more smood on camera movement.
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoodspeed);
        transform.position = smoothedPosition;

    }
}
