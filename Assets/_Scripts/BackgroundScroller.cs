using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour {

	enum Direction {LEFT, RIGHT, UP, DOWN};

	[SerializeField]
	List<Sprite> backgroundImages;
	[SerializeField]
	float scrollSpeed = 5f;
	[SerializeField]
	int tileSize = 25;
	[SerializeField]
	Direction scrollDirection = Direction.DOWN;
	[SerializeField]
	GameObject tilePrefab;

	List<GameObject> backgroundTiles;
	RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = this.GetComponent<RectTransform>();
		if (backgroundImages != null)
		{
			backgroundTiles = new List<GameObject>();
			GenerateBackground();
		}
	}


	private void Update()
	{
		if (backgroundTiles != null)
		{
			ScrollTiles();
		}
	}

	void ScrollTiles()
	{
		Vector3 scrollOffset = Vector3.zero;

		switch(scrollDirection)
		{
			case Direction.LEFT:
				scrollOffset = new Vector3(-scrollSpeed, 0);
				break;
			case Direction.RIGHT:
				scrollOffset = new Vector3(scrollSpeed, 0);
				break;
			case Direction.UP:
				scrollOffset = new Vector3(0,scrollSpeed);
				break;
			case Direction.DOWN:
				scrollOffset = new Vector3(0,-scrollSpeed);
				break;
		}

		foreach (GameObject tile in backgroundTiles)
		{
			tile.transform.position += scrollOffset;

			switch (scrollDirection)
			{
				case Direction.LEFT:
					if (tile.transform.position.x < -rt.rect.width / 2 + tileSize)
					{
						tile.transform.position = new Vector3(rt.rect.width / 2, tile.transform.position.y, tile.transform.position.z);
					}
					break;
				case Direction.RIGHT:
					if (tile.transform.position.x > rt.rect.width / 2 - tileSize)
					{
						tile.transform.position = new Vector3(-rt.rect.width / 2, tile.transform.position.y, tile.transform.position.z);
					}
					break;
				case Direction.UP:
					if (tile.transform.position.y > rt.rect.height / 2 - tileSize)
					{
						tile.transform.position = new Vector3(tile.transform.position.x, -rt.rect.height / 2, tile.transform.position.z);
					}
					break;
				case Direction.DOWN:
					if (tile.transform.position.y < -rt.rect.height / 2 + tileSize)
					{
						tile.transform.position = new Vector3(tile.transform.position.x, rt.rect.height / 2, tile.transform.position.z);
					}
					break;
			}
		}
	}

	void GenerateBackground()
	{
		float height = rt.rect.height;
		float width = rt.rect.width;

		int numVerticalTiles = (int)height / tileSize;
		int numHorizontalTiles = (int)width / tileSize;

		for (int j = 0; j < numVerticalTiles; j++)
		{
			for (int i = 0; i < numHorizontalTiles; i++)
			{
				GameObject go = Instantiate(tilePrefab) as GameObject;
				Image image = go.GetComponent<Image>();
				image.rectTransform.sizeDelta = new Vector2(tileSize, tileSize);
				image.sprite = backgroundImages[Random.Range(0, backgroundImages.Count - 1)];
				go.transform.position = new Vector3(i * tileSize - (width / 2 - tileSize/2), j * tileSize - (height / 2 - tileSize/2), 50);
				go.transform.SetParent(this.transform);
				backgroundTiles.Add(go);
			}
		}
		
	}
	
}
