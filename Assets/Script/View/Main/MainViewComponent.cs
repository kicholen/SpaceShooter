using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainViewComponent : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    const int fastSwipeMaxFrames = 30;
    const float fastSwipeMinThreshold = 30.0f;
    const float switchSpeed = 8f;

    ScrollRect scrollRect;
    RectTransform content;
    int screenCount;

    Vector2 startDragPosition;
    Vector2 targetPosition;
    int fastSwipeCount;
    bool isDragging;
    bool shouldSwitch;
    bool isBlocked;

    int currentScreen = (int)PanelType.PLAY - 1;

    List<Vector2> positions = new List<Vector2>();

    public void SwitchToScreen(PanelType screen)
    {
        currentScreen = (int)screen - 1;
        shouldSwitch = true;
        isDragging = false;
        isBlocked = true;
        targetPosition = getScreenPosition(currentScreen);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isBlocked)
        {
            startDragPosition = content.localPosition;
            fastSwipeCount = 0;
            isDragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isBlocked)
        {
            isDragging = false;
            shouldSwitch = true;

            if (isFastSwipe())
                fastSwipeSwitchToScreen();
            else
                switchToClosestScreen();
        }
    }

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        screenCount = content.childCount;
        createPositionsList();
    }

    void Update()
    {
        if (canSwitch())
            preformSwitch();

        if (isDragging)
            fastSwipeCount++;
    }

    bool canSwitch()
    {
        return !isDragging && shouldSwitch;
    }

    void preformSwitch()
    {
        content.localPosition = Vector2.Lerp(content.localPosition, targetPosition, switchSpeed * Time.deltaTime);
        if (Vector2.Distance(content.localPosition, targetPosition) < 0.005f)
        {
            content.localPosition = targetPosition;
            shouldSwitch = false;
            isBlocked = false;
        }
    }

    void switchToClosestScreen()
    {
        targetPosition = Vector3.zero;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < screenCount; i++)
        {
            float distance = Vector2.Distance(getScreenPosition(i), content.localPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPosition = getScreenPosition(i);
                currentScreen = i;
            }
        }
    }

    bool isFastSwipe()
    {
        return fastSwipeCount <= fastSwipeMaxFrames && Mathf.Abs(startDragPosition.x - content.localPosition.x) > fastSwipeMinThreshold;
    }

    void fastSwipeSwitchToScreen()
    {
        if (startDragPosition.x - content.localPosition.x > 0.0f)
            tryToMoveToNextScreen();
        else
            tryToMoveToPreviousScreen();
    }

    void tryToMoveToPreviousScreen()
    {
        if (currentScreen - 1 >= 0)
        {
            currentScreen = currentScreen - 1;
            targetPosition = getScreenPosition(currentScreen);
        }
    }

    void tryToMoveToNextScreen()
    {
        if (currentScreen + 1 < screenCount)
        {
            currentScreen = currentScreen + 1;
            targetPosition = getScreenPosition(currentScreen);
        }
    }

    Vector2 getScreenPosition(int screen)
    {
        return positions[screen];
    }

    void createPositionsList()
    {
        for (int i = 0; i < screenCount; ++i)
        {
            scrollRect.horizontalNormalizedPosition = i / (float)(screenCount - 1);
            positions.Add(content.localPosition);
        }
        scrollRect.horizontalNormalizedPosition = (float)currentScreen / (screenCount - 1);
    }
}
