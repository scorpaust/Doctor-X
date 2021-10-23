using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    
    public Texture2D cursorTexture;

    private Vector2 cursorHotspot;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;

			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

		ShowMouse();
    }

	public void ShowMouse()
	{
		Cursor.visible = true;

		Cursor.lockState = CursorLockMode.Confined;
	}

	public void HideMouse()
	{
		Cursor.visible = false;

		Cursor.lockState = CursorLockMode.None;
	}


}
