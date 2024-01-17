using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorArrow;

    private Vector2 cursorOn;
    void Start()
    {
        cursorOn =  new Vector2(cursorArrow.width/2, cursorArrow.height/2);
        Cursor.SetCursor(cursorArrow, cursorOn, CursorMode.ForceSoftware);
    }

}
