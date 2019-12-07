using UnityEngine;

public class C_CursorManager : MonoBehaviour
{
    [SerializeField]
    private CursorsHolder cursorsData;

    public Cursors cursors;
    public static C_CursorManager instance;
    public float _totalAnimTimeInSeconds = 1;

    GameObject activeCursor;
    CursorHandler cursorHandler;

    [System.Serializable]
    public struct CustomCursor
    {
        public Texture2D[] Texture;
        public Vector2 HotSpot;
        public CursorMode Mode;
        public float time;
    }

    [System.Serializable]
    public class CursorsHolder
    {
        [SerializeField]
        private CustomCursor defaultCursor;
        [SerializeField]
        private CustomCursor set;
        [SerializeField]
        private CustomCursor get;
        [SerializeField]
        private CustomCursor talk;
        [SerializeField]
        private CustomCursor search;
        [SerializeField]
        private CustomCursor up;
        [SerializeField]
        private CustomCursor down;
        [SerializeField]
        private CustomCursor left;
        [SerializeField]
        private CustomCursor right;

        public CustomCursor DefaultCursor { get { return defaultCursor; } }
        public CustomCursor Set { get { return set; } }
        public CustomCursor Get { get { return get; } }
        public CustomCursor Talk { get { return talk; } }
        public CustomCursor Search { get { return search; } }
        public CustomCursor Up { get { return up; } }
        public CustomCursor Down { get { return down; } }
        public CustomCursor Left { get { return left; } }
        public CustomCursor Right { get { return right; } }


        public void InitializeDefault(CustomCursor defaultCursor)
        {
            this.defaultCursor = defaultCursor;
        }
    }

    public enum Cursors
    {
        defaultCursor,
        set,
        get,
        talk,
        search,
        up,
        down,
        left,
        right
    }

    public interface ICursorHandler
    {
        Texture2D CurrentCursor { get; }
        void SetCursor(CustomCursor newCursor);
    }

    public class CursorHandler
    {
        private CustomCursor currentCursor;

        public CustomCursor CurrentCursor { get { return currentCursor; } }
        public float passedTime;
        public int currentNumber;

        public void SetCursor(CustomCursor newCursor)
        {
            Cursor.visible = true;
            passedTime = 0;
            currentCursor = newCursor;
            Cursor.SetCursor(currentCursor.Texture[0], currentCursor.HotSpot, currentCursor.Mode);
        }

        public void HideCursor()
        {
            Cursor.visible = false;
        }

        public void Animate()
        {
            float singleAnimTime = currentCursor.time / currentCursor.Texture.Length;
            passedTime += Time.deltaTime;

            if (passedTime >= singleAnimTime)
            {
                currentNumber++;
                if (currentNumber >= currentCursor.Texture.Length) { currentNumber = 0; }

                passedTime -= singleAnimTime;
                Cursor.SetCursor(currentCursor.Texture[currentNumber], currentCursor.HotSpot, currentCursor.Mode);                
            }
        }
    }

    private void Awake()
    {
        cursorHandler = new CursorHandler();
        cursorHandler.SetCursor(cursorsData.DefaultCursor);

        instance = this;
    }

    private void Update()
    {
        cursorHandler.Animate();
        //if (Input.GetMouseButtonDown(0))
        //    GetComponent<AudioSource>().Play();
    }

    public void UpdateTo(Cursors so)
    {
        cursorHandler.HideCursor();

        if (activeCursor) activeCursor.SetActive(false); activeCursor = null;
        switch (so)
        {
            case Cursors.get:
                cursorHandler.SetCursor(cursorsData.Get);
                break;

            case Cursors.set:
                cursorHandler.SetCursor(cursorsData.Set);
                break;

            case Cursors.search:
                cursorHandler.SetCursor(cursorsData.Search);
                break;

            case Cursors.talk:
                cursorHandler.SetCursor(cursorsData.Talk);
                break;

            case Cursors.left:
                cursorHandler.SetCursor(cursorsData.Left);
                break;
            case Cursors.right:
                cursorHandler.SetCursor(cursorsData.Right);
                break;
            case Cursors.up:
                cursorHandler.SetCursor(cursorsData.Up);
                break;
            case Cursors.down:
                cursorHandler.SetCursor(cursorsData.Down);
                break;

            default:
                cursorHandler.SetCursor(cursorsData.DefaultCursor);
                break;
        }
    }

    public void UpdateToDefault()
    {
        cursorHandler.SetCursor(cursorsData.DefaultCursor);
    }
}