using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainViewComponent : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    const int fastSwipeMaxFrames = 30;
    const float fastSwipeMinThreshold = 30.0f;
    const float switchSpeed = 8f;
    const float animDuration = 0.5f;

    public Action<PanelType> PanelSwitched;

    ScrollRect scrollRect;
    RectTransform content;
    int panelCount;

    Vector2 startDragPosition;
    Vector2 targetPosition;
    int fastSwipeCount;
    bool isDragging;
    bool shouldSwitch;
    bool isBlocked;

    int currentPanel = (int)PanelType.PLAY - 1;

    List<Vector2> positions = new List<Vector2>();
    float animTime;

    public void SwitchToPanel(PanelType panel)
    {
        currentPanel = (int)panel - 1;
        shouldSwitch = true;
        isDragging = false;
        isBlocked = true;
        targetPosition = getPanelPosition(currentPanel);
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
                fastSwipeSwitchToPanel();
            else
                switchToClosestPanel();
        }
    }

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        panelCount = content.childCount;
        createPositionsList();
    }

    void Update()
    {
        if (canSwitch())
            preformSwitch();
        else
            animTime = 0.0f;

        if (isDragging)
            fastSwipeCount++;
    }

    bool canSwitch()
    {
        return !isDragging && shouldSwitch;
    }

    void preformSwitch()
    {
        animTime += Time.deltaTime;
        content.localPosition = Vector2.Lerp(content.localPosition, targetPosition, animTime / animDuration);
        if (Vector2.Distance(content.localPosition, targetPosition) < 0.05f)
        {
            content.localPosition = targetPosition;
            shouldSwitch = false;
            isBlocked = false;
            PanelSwitched((PanelType)(currentPanel + 1));
        }
    }

    void switchToClosestPanel()
    {
        targetPosition = Vector3.zero;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < panelCount; i++)
        {
            float distance = Vector2.Distance(getPanelPosition(i), content.localPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPosition = getPanelPosition(i);
                currentPanel = i;
            }
        }
    }

    bool isFastSwipe()
    {
        return fastSwipeCount <= fastSwipeMaxFrames && Mathf.Abs(startDragPosition.x - content.localPosition.x) > fastSwipeMinThreshold;
    }

    void fastSwipeSwitchToPanel()
    {
        if (startDragPosition.x - content.localPosition.x > 0.0f)
            tryToMoveToNextPanel();
        else
            tryToMoveToPreviousPanel();
    }

    void tryToMoveToPreviousPanel()
    {
        if (currentPanel - 1 >= 0)
        {
            currentPanel = currentPanel - 1;
            targetPosition = getPanelPosition(currentPanel);
        }
    }

    void tryToMoveToNextPanel()
    {
        if (currentPanel + 1 < panelCount)
        {
            currentPanel = currentPanel + 1;
            targetPosition = getPanelPosition(currentPanel);
        }
    }

    Vector2 getPanelPosition(int panel)
    {
        return positions[panel];
    }

    void createPositionsList()
    {
        for (int i = 0; i < panelCount; ++i)
        {
            scrollRect.horizontalNormalizedPosition = i / (float)(panelCount - 1);
            positions.Add(content.localPosition);
        }
        scrollRect.horizontalNormalizedPosition = (float)currentPanel / (panelCount - 1);
    }
}
