using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelClearControler : MonoBehaviour
{
    [Tooltip("By Default, Loads MainMenu")]
    [SerializeField] int sceneToLoadWhenClicked = 1;
    [SerializeField] float textSlideInSpeed = 2;
    [SerializeField] float backgroundFadeInSpeed = 2;
    [SerializeField] float buttonFadeInSpeed = 2;
    [Header("References")]
    [SerializeField] Image backGroundFadeIn;
    [SerializeField] GameObject textSlideIn1;
    [SerializeField] GameObject textSlideIn2;
    [SerializeField] Image buttonFadeIn;
    static Canvas canvas;
    static bool runLevelClearAnimation = false;
    static GameObject buttonRef;
    Vector2 textSlideIn1StartPosition;
    Vector2 textSlideIn2StartPosition;
    TextMeshProUGUI buttonTextFadeIn;
    //value storage
    int animationStage = 0;
    float bgFadeAlpha = 0;
    static EventSystem eventSystem;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        buttonTextFadeIn = buttonFadeIn.GetComponentInChildren<TextMeshProUGUI>();
        textSlideIn1StartPosition = textSlideIn1.transform.position;
        textSlideIn2StartPosition = textSlideIn2.transform.position;
        textSlideIn1.transform.position = new Vector2(textSlideIn1.transform.position.x, textSlideIn1.transform.position.y + 700);
        textSlideIn2.transform.position = new Vector2(textSlideIn2.transform.position.x, textSlideIn2.transform.position.y - 700);
        backGroundFadeIn.color = new Color(backGroundFadeIn.color.r, backGroundFadeIn.color.g, backGroundFadeIn.color.b, 0);
        buttonFadeIn.color = new Color(1, 1, 1, 0);
        buttonTextFadeIn.color = new Color(1, 1, 1, 0);
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        buttonRef = buttonFadeIn.gameObject;
    }
    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoadWhenClicked);
        Debug.Log("loaded scene " + sceneToLoadWhenClicked);
    }
    public static void TriggerLevelClearScrene()
    {
        PauseMenuControler.DisablePauseMenuOpening();
        canvas.enabled = true;
        Time.timeScale = 0;
        runLevelClearAnimation = true;
        eventSystem.sendNavigationEvents = true;
        eventSystem.firstSelectedGameObject = buttonRef;
        Debug.Log("im running");
    }
    private void Update()
    {
        if (runLevelClearAnimation)
        {
            if (animationStage == 0)
            {
                backGroundFadeIn.color = new Color(backGroundFadeIn.color.r, backGroundFadeIn.color.g, backGroundFadeIn.color.b, backGroundFadeIn.color.a + (backgroundFadeInSpeed * Time.unscaledDeltaTime));
                if (backGroundFadeIn.color.a >= 1)
                    animationStage++;
            }
            else if (animationStage == 1)
            {
                textSlideIn1.transform.position = Vector2.MoveTowards(textSlideIn1.transform.position, textSlideIn1StartPosition, textSlideInSpeed * Time.unscaledDeltaTime * 100);
                textSlideIn2.transform.position = Vector2.MoveTowards(textSlideIn2.transform.position, textSlideIn2StartPosition, textSlideInSpeed * Time.unscaledDeltaTime * 100);
                if (Vector2.Distance(textSlideIn1.transform.position, textSlideIn1StartPosition) == 0)
                    animationStage++;
            }
            else if (animationStage == 2)
            {
                Color currentColor = new Color(1, 1, 1, buttonFadeIn.color.a + buttonFadeInSpeed * Time.unscaledDeltaTime);
                buttonFadeIn.color = currentColor;
                buttonTextFadeIn.color = currentColor;
                if (buttonFadeIn.color.a >= 1)
                    animationStage++;
            }
            //stops animation check when animation complete
            if (animationStage == 3)
                runLevelClearAnimation = false;
        }
    }
}
