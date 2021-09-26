using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves")]
    [SerializeField] private float _speed = 30f;
    [SerializeField] private float _xRange = 17f;
    [SerializeField] private float _yRangeMax = 18f;
    [SerializeField] private float _yRangeMin = -4.5f;
    [SerializeField] GameObject[] lasers;

    [Header("Screen Position Tuning")]
    [SerializeField] private float _pitchFactor = -2f;
    private float _pitchControl = -10f;
    [SerializeField] private float _yawFactor = 2f;
    [SerializeField] private float _rollFactor = -20f;

    private float xThrow, yThrow, zThrow;
    private void Start()
    {

    }

    private void Update()
    {
       ProcessTranslation();
       ProcessRotation();
       ProcessFiring();
    }

    private void ProcessFiring()
    {
    
       if (Input.GetButton("Fire1"))
       {
           SetLasersActive(true);
       }
       else
       {
           SetLasersActive(false);
       }
    }

    private void SetLasersActive(bool active)
    {
        foreach (GameObject laser in lasers)
        {
           // laser.SetActive(true);
           var emissionModule = laser.GetComponent<ParticleSystem>().emission;
           emissionModule.enabled = active;
        }
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffSet = xThrow * _speed * Time.deltaTime;
        float yOffset = yThrow * _speed * Time.deltaTime;

        float xNewPosition = transform.localPosition.x + xOffSet;
        float yNewPosition = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(xNewPosition, -_xRange, _xRange);
        float clampedYpos = Mathf.Clamp(yNewPosition, _yRangeMin, _yRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYpos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {    
        float pitchDueToPosition = transform.localPosition.y * _pitchFactor;
        float pitchDueToControlThrow = yThrow * _pitchControl;  
        float xParam = pitchDueToPosition + pitchDueToControlThrow; // pitch

        float yParam = transform.localPosition.x * _yawFactor;  // yaw
        float zParam = xThrow * _rollFactor;  // roll

        transform.localRotation = Quaternion.Euler(xParam, yParam, zParam);
    }
}
