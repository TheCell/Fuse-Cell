using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactionlogic : MonoBehaviour
{
	private int playernr = 0;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if (hit.collider != null)
			{
				Vector3 hitPoint = hit.collider.transform.InverseTransformPoint(hit.point);
				//Debug.Log("tile clicked at: " + hitPoint.x + " " + hitPoint.y + " " + hitPoint.z);
				Tilelogic tile = hit.collider.gameObject.GetComponent<Tilelogic>();
				if (tile != null)
				{
					//int playernr = (int)Mathf.Round(Random.Range(0, 1.1f));
					
					tile.TileClicked(playernr, new Vector2(hitPoint.x, hitPoint.y));
					if (playernr == 0)
					{
						playernr = 1;
					}
					else if (playernr == 1)
					{
						playernr = 0;
					}
				}
			}
		}
	}
}
