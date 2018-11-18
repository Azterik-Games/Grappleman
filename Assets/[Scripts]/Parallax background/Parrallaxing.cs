using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Azterik.Mechanics.Parallaxing
{
    public class Parrallaxing : MonoBehaviour
    {
        [SerializeField] Transform[] backgrounds;
        float[] parallaxScales;
        [SerializeField] float smoothing = 1f;

        Transform cam;
        Vector3 previousCamPos;

        void Start()
        {
            cam = Camera.main.transform;

            previousCamPos = cam.position;

            parallaxScales = new float[backgrounds.Length];
            for (int i = 0; i < backgrounds.Length; i++)
            {
                parallaxScales[i] = backgrounds[i].position.z * -1;
            }
        }

        void Update()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
                float backgroundTargetPosX = backgrounds[i].position.x + parallax;

                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

            previousCamPos = cam.position;
        }
    }
}