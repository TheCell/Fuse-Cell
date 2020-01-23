using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactionlogic : MonoBehaviour
{
	private GameSettingsAccess gameSettingsAccess;
	private int currentPlayer = 0;
	private WinTracker wintracker;

	// Start is called before the first frame update
	private void Start()
    {
		gameSettingsAccess = Camera.main.GetComponent<GameSettingsAccess>();
		wintracker = Camera.main.GetComponent<WinTracker>();
	}

	// Update is called once per frame
	private void Update()
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
					bool aTileClosed = false;
					tile.TileClicked(currentPlayer, new Vector2(hitPoint.x, hitPoint.y), out aTileClosed);

					if (!aTileClosed)
					{
						//Debug.Log("updateActivePlayer");
						UpdateActivePlayer();
					}
					else
					{
						wintracker.CheckWinCondition();
					}
				}
			}
		}
	}

	private void UpdateActivePlayer()
	{
		currentPlayer++;
		if (currentPlayer >= gameSettingsAccess.GetAmountOfPlayers())
		{
			currentPlayer = 0;
		}
	}
}
