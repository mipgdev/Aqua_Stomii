using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollider : MonoBehaviour
{
    private PolygonCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<PolygonCollider2D>();
    }

    public void SetOffsetX(float offX)
    {
        if (boxCollider != null)
        {
            boxCollider.offset = new Vector2(offX, boxCollider.offset.y);
        }
    }
}
