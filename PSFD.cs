using HutongGames.PlayMaker;
using System.Collections;
using UnityEngine;
using MSCLoader;
using System;

public class Paintable : MonoBehaviour
{
    //Read values from held can
    PlayMakerFSM can;
    FsmFloat canLvl;
    FsmInt canType;
    FsmColor canColor;
    FsmGameObject PaintedObject;

    //Object itself
    public Material[] paintTypes;
    public int paintType;
    public Color PaintColor = Color.white;

    public Renderer rendererIndex;

    GameObject canObj;
    bool waitingPaint;

    void Start()
    {
        canObj = GameObject.Find("PLAYER").transform.Find("Pivot/AnimPivot/Camera/FPSCamera/SprayCan").gameObject;
        can = canObj.GetComponent<PlayMakerFSM>();
        canLvl = can.FsmVariables.FindFsmFloat("Fluid");
        canType = can.FsmVariables.FindFsmInt("PaintType");
        canColor = can.FsmVariables.FindFsmColor("SprayColor");
        PaintedObject = can.FsmVariables.FindFsmGameObject("PaintedPart");
    }

    IEnumerator PaintWait()
    {
        waitingPaint = canLvl.Value > 1 ? true : false;
        yield return new WaitForSeconds(3f);
        if (waitingPaint) ApplyPaintSave(canColor.Value, canType.Value);
    }

    public void ApplyPaintSave(Color color, int type)
    {
        try
        {
            rendererIndex.material = paintTypes[type];
            rendererIndex.material.color = color;
            PaintColor = color;
            paintType = type;
            waitingPaint = false;
        }
        catch (Exception ex)
        {
            ModConsole.Error($"Failed to paint {name}");
            ModConsole.Error($"{ex}");
        }
    }

    void Update()
    {
        if (canObj.activeInHierarchy && Input.GetMouseButton(0))
        {
            if (PaintedObject.Value == gameObject && !waitingPaint)
                StartCoroutine(PaintWait());
        }
        else if (waitingPaint) PaintCancel();
    }

    void PaintCancel()
    {
        StopCoroutine(PaintWait());
        waitingPaint = false;
    }
}
