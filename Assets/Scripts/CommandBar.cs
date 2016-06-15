using UnityEngine;
using System.Collections;


public class CommandBar : MonoBehaviour
{
    private CommandButton[] commandButtons;

    public float buttonSize = 1.28f;
    public float buttonRows = 1f;
    public float buttonColumns = 6f;
    public float buttonRowSpacing = 0;
    public float buttonColumnSpacing = 0;

    public bool anchor = true;
    public Vector2 anchorOffset = Vector2.zero;
    public ScreenPositionAnchorPoint anchorPoint = ScreenPositionAnchorPoint.BottomCenter; 

    public Sprite DefaultButtonImage;
    public Sprite SelectedButtonImage;

    private float ScreenHeight;
    private float ScreenWidth;

    public int Layer
    {
        get {
                return gameObject.layer;
            }
    }
	
    float Width
    {
        get
        {
            return (buttonSize * buttonColumns) + Mathf.Clamp((buttonColumnSpacing * (buttonColumns - 1)), 0, int.MaxValue); // CHE SENSO HA??
        }
    }

    float Height
    {
        get
        {
            return (buttonSize * buttonRows) + Mathf.Clamp((buttonRowSpacing * (buttonRows - 1)), 0, int.MaxValue); // CHE SENSO HA??
        }
    }

    public enum ScreenPositionAnchorPoint
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    void Awake()
    {
        
        ScreenHeight = Camera.main.orthographicSize * 2;
        ScreenWidth = ScreenHeight * ScreenWidth / Screen.height; 
    }

    void Start()
    {
        InitCommandButtons();
    }

    void Update()
    {
        Vector2 position = Vector2.zero;

        if (anchor)
            position = CalculateAnchorScreenPosition();
        else
            position = transform.position;

        SetPosition(position.x, position.y);
    }

    CommandButton CreateButton()
    {
        GameObject go = new GameObject("CommandButton");

        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>();
        go.transform.parent = transform;

        CommandButton button = go.AddComponent<CommandButton>();
        button.Init(this); //SCAMBIO RECIPROCO
        return button;
    }

    void InitButtonPosition()
    {
        int i = 0;
        float xPos = 0;
        float yPos = 0;

        for (int r = 0; r < buttonRows; ++r)
        {
            xPos = 0;
            for(int c = 0; c < buttonColumns; ++c)
            {
                commandButtons[i].transform.localScale = new Vector3(buttonSize, buttonSize, 0);
                commandButtons[i].transform.localPosition = new Vector3(xPos, yPos, 0);
                i++;
                xPos += buttonSize + buttonColumnSpacing; 
            }
            yPos -= buttonSize + buttonRowSpacing;
        }

    }
    
    void InitCommandButtons()
    {
        commandButtons = new CommandButton[(int)buttonRows * (int)buttonColumns];

        for(int i = 0; i < commandButtons.Length; ++i)
        {
            var newButton = CreateButton();

            if (i < GameState.currentPlayer.Inventory.Count)
                newButton.AddInventoryItem(GameState.currentPlayer.Inventory[i]);

            commandButtons[i] = newButton;
        }

        InitButtonPosition();
    }

    void SetPosition(float x,float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    Vector2 CalculateAnchorScreenPosition()
    {
        Vector2 position = Vector2.zero;

        switch (anchorPoint)
        {
            case ScreenPositionAnchorPoint.TopLeft:
                position.y = (ScreenHeight / 2) - Height;
                position.x = (ScreenWidth / 2) - Width;
                break;
            case ScreenPositionAnchorPoint.TopCenter:
                position.y = (ScreenHeight / 2) - Height;
                position.x = -(Width / 2);
                break;
            case ScreenPositionAnchorPoint.TopRight:
                position.y = (ScreenHeight / 2) - Height;
                position.x = -(ScreenWidth / 2) + buttonSize; 
                break;
            case ScreenPositionAnchorPoint.MiddleLeft:
                position.y = (Height / 2) ;
                position.x = (ScreenWidth / 2) - Width;
                break;
            case ScreenPositionAnchorPoint.MiddleCenter:
                position.y = (Height / 2) ;
                position.x = -(Width / 2) ;
                break;
            case ScreenPositionAnchorPoint.MiddleRight:
                position.y = (Height / 2);
                position.x = -(ScreenWidth / 2) + buttonSize; 
                break;
            case ScreenPositionAnchorPoint.BottomLeft:
                position.y = -(ScreenHeight / 2) + Height;
                position.x = (ScreenWidth / 2) - Width;
                break;
            case ScreenPositionAnchorPoint.BottomCenter:
                position.y = -(ScreenHeight / 2) + Height;
                position.x = -(Width / 2) ;
                break;
            case ScreenPositionAnchorPoint.BottomRight:
                position.y = -(ScreenHeight / 2) + Height;
                position.x = -(ScreenWidth / 2) + buttonSize; 
                break;
            default:
                break;
        }
        return anchorOffset + position;
    }
}
