using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azterik.Mechanics.Parallaxing
{
    public class Tiling : MonoBehaviour
    {
        [SerializeField] Vector2 offset = new Vector2(2, 0);

        bool rightNeighbour = false;
        bool leftNeighbour = false;

        bool reverseScale = false;

        float width = 0f;
        Camera cam;

        void Start()
        {
            SpriteRenderer s = GetComponent<SpriteRenderer>();
            width = s.sprite.bounds.size.x;
        }

        void Update()
        {
            if(!leftNeighbour || !rightNeighbour)
            {
                float camHor = cam.orthographicSize * Screen.width / Screen.height;
                float edgeVisibleLeft = (transform.position.x + width / 2) - camHor;
                float edgeVisibleRight = (transform.position.x - width / 2) - camHor;

                if(cam.transform.position.x >= edgeVisibleRight + offset.x && !rightNeighbour)
                {

                }
                else if(cam.transform.position.x <= edgeVisibleRight - offset.x && !leftNeighbour)
                {

                }
            }
        }

        void NewNeighbour (int side)
        {
            Vector3 newPos = new Vector3(transform.position.x + width * side, transform.position.y, transform.position.z);
            Transform neighbour = Instantiate(transform, newPos, transform.rotation) as Transform;

            if (reverseScale)
                neighbour.localScale = new Vector3(neighbour.localScale.x * -1, neighbour.localScale.y, neighbour.localScale.z);

            neighbour.parent = transform.parent;
        }
    }
}