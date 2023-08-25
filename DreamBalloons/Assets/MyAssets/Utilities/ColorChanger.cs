using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using SpriteGlow;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    [Header("__________________________________General__________________________________")]
    public Color newColor;
    [HideInInspector] public Color oldColor;
    [HideInInspector] public Color lerpedColor;
    public bool dontChangeThis;
    
    public float durationChange;
    public float DurationChange { get { return durationChange; } set { durationChange = value;} }
    public bool changeAtStartGame;
    public bool firstChangeInstantly;
    public bool changeOnlyAlfa;
    public bool changeWithoutAlfa;
    private bool disableAfterHide;
    public bool DisableAfterHide { get { return disableAfterHide; } set { disableAfterHide = value; } }

    [Header("_________________________________Auto Return_________________________________")]
    [ConditionalHide("autoReturnToOldColor", false, true)]
    public bool autoReturnToNewColor;
    [ConditionalHide("autoReturnToNewColor", false, true)]
    public bool autoReturnToOldColor;
    [ConditionalHide("autoReturnToNewColor", UseOrLogic = true, ConditionalSourceField2 = "autoReturnToOldColor")]
    public float durationReturn;

    [Header("______________________________Children Settings_________________________________")]
    public bool changeTogetherWithChildren;
    public bool copyParamToChildren;

    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;
    private Text textUI;
    private Image imageUI;
    // private Light2D lightSource;
    private Outline outline;
    private SpriteShapeRenderer spriteShapeRenderer;
    private SpriteGlow.SpriteGlowEffect spriteGlowEffect;
    private TextMeshProUGUI textMeshProUGUI;

    [HideInInspector] 
    public float t = 0f;
    [HideInInspector]
    public float stepPerSecond;
    private bool componentIsFound;
    private bool itNotFirstChange;
    private bool wasFirstStart;

    public bool useUnscaledDeltaTime;

    #region UtilityForUser
    private void OnValidate()
    {
        if (durationChange <= 0)
            durationChange = 0.1f;

        if (changeOnlyAlfa && changeWithoutAlfa)
        {
            changeOnlyAlfa = false;
            changeWithoutAlfa = false;
        }

        if (copyParamToChildren && transform.childCount > 0)
        {
            CopyToChild();
        }
    }

    public void CopyToChild()
    {
        foreach (Transform child in transform)
        {
            if (!child.GetComponent<ColorChanger>())     
                child.gameObject.AddComponent<ColorChanger>();

            child.GetComponent<ColorChanger>().copyParamToChildren = copyParamToChildren;
            child.GetComponent<ColorChanger>().newColor = newColor;
            child.GetComponent<ColorChanger>().durationChange = durationChange;
            child.GetComponent<ColorChanger>().changeAtStartGame = changeAtStartGame;
            child.GetComponent<ColorChanger>().firstChangeInstantly = firstChangeInstantly;
            child.GetComponent<ColorChanger>().changeOnlyAlfa = changeOnlyAlfa;
            child.GetComponent<ColorChanger>().changeWithoutAlfa = changeWithoutAlfa;
            child.GetComponent<ColorChanger>().autoReturnToNewColor = autoReturnToNewColor;
            child.GetComponent<ColorChanger>().autoReturnToOldColor = autoReturnToOldColor;
            child.GetComponent<ColorChanger>().durationReturn = durationReturn;
            child.GetComponent<ColorChanger>().changeTogetherWithChildren = changeTogetherWithChildren;            
            child.GetComponent<ColorChanger>().useUnscaledDeltaTime = useUnscaledDeltaTime;            

            if (child.childCount > 0)
                child.GetComponent<ColorChanger>().CopyToChild();
        }
    }

    public void DeleteSelfInChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<ColorChanger>(out ColorChanger childColorChanger))
                childColorChanger.DeleteSelfInChildren();
        }
        DestroyImmediate(this);
    }
    #endregion

    private void Awake()
    {        
        GetOldColor(transform);
        lerpedColor = oldColor;

        if (!changeAtStartGame)
            enabled = false;
        else
        {
            StartChange();            
        }
    }

    private void Update()
    {
        SetLerpedColor();
        CheckResults();
    }    

    private void CheckResults()
    {
        if (!componentIsFound)
        {
            enabled = false;
        }

        if(t == 1)
        {
            if(autoReturnToOldColor)
            {
                StartChange();
                stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationReturn;                
            }
            else
                enabled = false;
            if (stepPerSecond < 0 && disableAfterHide)
                gameObject.SetActive(false);
        }
        if(t == 0)
        {
            if(autoReturnToNewColor)
            {
                StartChange();
                stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationReturn;
            }
            else
                enabled = false;
            if (stepPerSecond < 0 && disableAfterHide)
                gameObject.SetActive(false);
        }

        // if (t == 1 || t == 0)
        // {
        //     enabled = false;
        //     if (stepPerSecond < 0 && disableAfterHide)
        //         gameObject.SetActive(false);
        // }
    }

    public void GetOldColor(Transform target)
    {
        if (target.GetComponent<SpriteGlowEffect>())
        {
            spriteGlowEffect = target.GetComponent<SpriteGlowEffect>();
            oldColor = spriteGlowEffect.GlowColor;
            componentIsFound = true;
            //return;
        }
        if (target.GetComponent<SpriteRenderer>())
        {
            spriteRenderer = target.GetComponent<SpriteRenderer>();
            oldColor = spriteRenderer.color;
            componentIsFound = true;
            return;
        }
        if (target.GetComponent<TextMesh>())
        {
            textMesh = target.GetComponent<TextMesh>();
            oldColor = textMesh.color;
            componentIsFound = true;
            return;
        }
        if (target.GetComponent<Text>())
        {
            textUI = target.GetComponent<Text>();
            oldColor = textUI.color;
            componentIsFound = true;
            return;
        }
        if (target.GetComponent<Image>())
        {
            imageUI = target.GetComponent<Image>();
            oldColor = imageUI.color;
            componentIsFound = true;
            return;
        }
        // if (target.GetComponent<Light2D>())
        // {
        //     lightSource = target.GetComponent<Light2D>();
        //     oldColor = lightSource.color;
        //     componentIsFound = true;
        //     return;
        // }
        if (target.GetComponent<Outline>())
        {
            outline = target.GetComponent<Outline>();
            oldColor = outline.effectColor;
            componentIsFound = true;
            return;
        }
        if (target.GetComponent<SpriteShapeRenderer>())
        {
            spriteShapeRenderer = target.GetComponent<SpriteShapeRenderer>();
            oldColor = spriteShapeRenderer.color;
            componentIsFound = true;
            return;
        }
        if (target.GetComponent<TextMeshProUGUI>())
        {
            textMeshProUGUI = target.GetComponent<TextMeshProUGUI>();
            oldColor = textMeshProUGUI.color;
            componentIsFound = true;
            return;
        }

        componentIsFound = false;
    }    

    private void SetLerpedColor()
    {
        if (componentIsFound && !dontChangeThis)
        {
            GetLerpedColor(oldColor, newColor);

            if (spriteGlowEffect != null)
                spriteGlowEffect.GlowColor = lerpedColor;
            if (spriteRenderer != null)
                spriteRenderer.color = lerpedColor;
            if (textMesh != null)
                textMesh.color = lerpedColor;
            if (textUI != null)
                textUI.color = lerpedColor;
            if (imageUI != null)
                imageUI.color = lerpedColor;
            // if (lightSource != null)
            //     lightSource.color = lerpedColor;            
            if (outline != null)
                outline.effectColor = lerpedColor;
            if (spriteShapeRenderer != null)
                spriteShapeRenderer.color = lerpedColor;
            if (textMeshProUGUI != null)
                textMeshProUGUI.color = lerpedColor;
        }        
    }

    private Color GetLerpedColor(Color _oldColor, Color _newColor)
    {   
        if(useUnscaledDeltaTime)
            t += stepPerSecond * Time.unscaledDeltaTime;
        else
            t += stepPerSecond * Time.deltaTime;

        if (t > 1) t = 1;
        if (t < 0) t = 0;
        
        lerpedColor = Color.Lerp(_oldColor, _newColor, t);

        if (changeOnlyAlfa)
            lerpedColor = new Color(_oldColor.r, _oldColor.g, _oldColor.b, lerpedColor.a);

        if(changeWithoutAlfa)
            lerpedColor = new Color(lerpedColor.r, lerpedColor.g, lerpedColor.b, _oldColor.a);

        return lerpedColor;
    }

    public void SetNewColor (Color newTargetColor)
    {
        oldColor = lerpedColor;
        t = 0;
        //stepPerSecond = 1f / durationReturn;
        //wasFirstStart = false;
        newColor = newTargetColor;
    }

    public void StartChange()
    {
        stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationChange;

        if(wasFirstStart){
            stepPerSecond *= -1;
        }
        wasFirstStart = true;

        if (firstChangeInstantly)
        {
            if(stepPerSecond > 0)
                t = 1;
            if (stepPerSecond < 0)
                t = 0;
            firstChangeInstantly = false;
        }
        //Debug.Log("stepPerSecond = " + stepPerSecond);

        if (changeTogetherWithChildren && !changeAtStartGame)   //Второе условие стоит из-за того, что одновременное выполнение changeTogetherWithChildren и changeAtStartGame не допустимо, но допустимо последовательное
        { 
            foreach (Transform child in transform)
            {                
                child.GetComponent<ColorChanger>().StartChange();
            }
        }

        changeAtStartGame = false;

        if(durationChange == 0) //Чтобы смена цвета произошла в том же кадре, в котором и была вызвана.
        {
            SetLerpedColor();
            CheckResults();
        }
        else
            enabled = true;
    }

    public void StartChange(float newDurationChange)
    {
        stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / newDurationChange;

        if(wasFirstStart){
            stepPerSecond *= -1;
        }
        wasFirstStart = true;

        if (firstChangeInstantly)
        {
            if(stepPerSecond > 0)
                t = 1;
            if (stepPerSecond < 0)
                t = 0;
            firstChangeInstantly = false;
        }
        //Debug.Log("stepPerSecond = " + stepPerSecond);

        if (changeTogetherWithChildren && !changeAtStartGame)   //Второе условие стоит из-за того, что одновременное выполнение changeTogetherWithChildren и changeAtStartGame не допустимо, но допустимо последовательное
        { 
            foreach (Transform child in transform)
            {                
                child.GetComponent<ColorChanger>().StartChange(newDurationChange);
            }
        }

        changeAtStartGame = false;

        if(newDurationChange == 0) //Чтобы смена цвета произошла в том же кадре, в котором и была вызвана.
        {
            SetLerpedColor();
            CheckResults();
        }
        else
            enabled = true;
    }

    public void ChangeToT1()
    {
        stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationChange;

        if (stepPerSecond < 0)
        {
            stepPerSecond *= -1;
        }

        if (changeTogetherWithChildren && !changeAtStartGame)   //Второе условие стоит из-за того, что одновременное выполнение changeTogetherWithChildren и changeAtStartGame не допустимо, но допустимо последовательное
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<ColorChanger>().ChangeToT1();
            }
        }

        changeAtStartGame = false;

        if(durationChange == 0) //Чтобы смена цвета произошла в том же кадре, в котором и была вызвана.
        {
            SetLerpedColor();
            CheckResults();
        }
        else
            enabled = true;
    }

    public void ChangeToT0()
    {
        stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationChange;

        if (stepPerSecond > 0)
        {
            stepPerSecond *= -1;
        }

        if (changeTogetherWithChildren && !changeAtStartGame)   //Второе условие стоит из-за того, что одновременное выполнение changeTogetherWithChildren и changeAtStartGame не допустимо, но допустимо последовательное
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<ColorChanger>().ChangeToT0();
            }
        }

        changeAtStartGame = false;
        
        if(durationChange == 0) //Чтобы смена цвета произошла в том же кадре, в котором и была вызвана.
        {
            SetLerpedColor();
            CheckResults();
        }
        else
            enabled = true;
    }

    public void ChangeToT1(float newDurationChange)
    {
        durationChange = newDurationChange;
        stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationChange;

        if (stepPerSecond < 0)
        {
            stepPerSecond *= -1;
        }

        enabled = true;

        if (changeTogetherWithChildren && !changeAtStartGame)   //Второе условие стоит из-за того, что одновременное выполнение changeTogetherWithChildren и changeAtStartGame не допустимо, но допустимо последовательное
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<ColorChanger>().ChangeToT1(durationChange);
            }
        }

        changeAtStartGame = false;
        
        if(durationChange == 0) //Чтобы смена цвета произошла в том же кадре, в котором и была вызвана.
        {
            SetLerpedColor();
            CheckResults();
        }
        else
            enabled = true;
    }

    public void ChangeToT0(float newDurationChange)
    {
        durationChange = newDurationChange;
        stepPerSecond = Mathf.Sign(stepPerSecond) * 1f / durationChange;

        if (stepPerSecond > 0)
        {
            stepPerSecond *= -1;
        }

        if (changeTogetherWithChildren && !changeAtStartGame)   //Второе условие стоит из-за того, что одновременное выполнение changeTogetherWithChildren и changeAtStartGame не допустимо, но допустимо последовательное
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<ColorChanger>().ChangeToT0(durationChange);
            }
        }

        changeAtStartGame = false;

        if(durationChange == 0) //Чтобы смена цвета произошла в том же кадре, в котором и была вызвана.
        {
            SetLerpedColor();
            CheckResults();
        }
        else
            enabled = true;
    }

    public void ResetColorOfTarget(Transform target)
    {
        if (target.GetComponent<SpriteGlowEffect>())
        {
            spriteGlowEffect = target.GetComponent<SpriteGlowEffect>();
            spriteGlowEffect.GlowColor = lerpedColor;
            //return;
        }
        if (target.GetComponent<SpriteRenderer>())
        {
            spriteRenderer = target.GetComponent<SpriteRenderer>();
            spriteRenderer.color = lerpedColor;
            return;
        }
        if (target.GetComponent<TextMesh>())
        {
            textMesh = target.GetComponent<TextMesh>();
            textMesh.color = lerpedColor;
            return;
        }
        if (target.GetComponent<Text>())
        {
            textUI = target.GetComponent<Text>();
            textUI.color = lerpedColor;
            return;
        }
        if (target.GetComponent<Image>())
        {
            imageUI = target.GetComponent<Image>();
            imageUI.color = lerpedColor;
            return;
        }
        // if (target.GetComponent<Light2D>())
        // {
        //     lightSource = target.GetComponent<Light2D>();
        //     lightSource.color = lerpedColor;
        //     return;
        // }
        if (target.GetComponent<Outline>())
        {
            outline = target.GetComponent<Outline>();
            outline.effectColor = lerpedColor;
            return;
        }
        if (target.GetComponent<SpriteShapeRenderer>())
        {
            spriteShapeRenderer = target.GetComponent<SpriteShapeRenderer>();
            spriteShapeRenderer.color = lerpedColor;
            return;
        }
        if (target.GetComponent<TextMeshProUGUI>())
        {
            textMeshProUGUI = target.GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.color = lerpedColor;
            return;
        }
    }
}