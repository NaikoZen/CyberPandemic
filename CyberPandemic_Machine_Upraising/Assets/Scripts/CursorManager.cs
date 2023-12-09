using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class CursorManager : NetworkBehaviour
{
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Texture2D cursorOnFire;
    
    private Vector2 cursorHotspot;


    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    private void Update()
    {
        AttCursor();
    }

    public void AttCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Cursor.SetCursor(cursorOnFire, cursorHotspot, CursorMode.Auto);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        }
    }
   
}
