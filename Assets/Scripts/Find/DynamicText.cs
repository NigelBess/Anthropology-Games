using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicText : MonoBehaviour
{
 private Text target;
    private RectTransform rt;
    [SerializeField]private int initialSizeFactor;
    [SerializeField] private float effectTime;
    private float endTime;
    private void Awake()
    {
        target = GetComponent<Text>();
        rt = target.GetComponent<RectTransform>();
    }
    public void ChangeTo(string newText)
    {
        ChangeTo(newText,true);
    }
    public void ChangeTo(string newText,bool dynamic)
    {
        target.text = newText;
        if (dynamic)
        {
            enabled = true;
            rt.localScale = rt.localScale * initialSizeFactor;
            endTime = Time.time + effectTime;
        }
        else
        {
            enabled = false;
            rt.localScale = Vector3.one;
        }
    }
    private void Update()
    {
        if (Time.time < endTime)
        {
            rt.localScale = Vector3.one * Mathf.Lerp(1, initialSizeFactor, (endTime - Time.time)/effectTime);
        }
        else
        {
            rt.localScale = Vector3.one;
            enabled = false;
        }
    }
    public void StartCountDown(int num)
    {
        CountDown(num);
    }
    private void CountDown(int current)
    {
        if (current <= 0)
        {
            ChangeTo("<color=#00FF00>Go!</color>");
            return;
        }
        else
        {
            ChangeTo("<color=yellow>"+current.ToString()+"</color>");
            current--;
            StartCoroutine(CountDownRoutine(current));
        }
    }
    IEnumerator CountDownRoutine(int current)
    {
        yield return new WaitForSeconds(1f);
        CountDown(current);
    }
}
