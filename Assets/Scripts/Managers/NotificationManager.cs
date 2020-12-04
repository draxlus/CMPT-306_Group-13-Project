using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<NotificationManager>();


            if (instance != null)
            {
                return instance;
            }

            CreateNewInstance();

            return instance;
        }
    }

    public static NotificationManager CreateNewInstance()
    {
        NotificationManager notificationPrefab = Resources.Load<NotificationManager>("NotificationManager");
        instance = Instantiate(notificationPrefab);

        return instance;
    }

    private static NotificationManager instance;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Text notification;
    [SerializeField] private float fadeTime;

    private IEnumerator notificationCoroutine;

    public void SetNewNotification(string message)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }
        notificationCoroutine = FadeOutNotification(message);
        StartCoroutine(notificationCoroutine);
    }

    private IEnumerator FadeOutNotification(string message)
    {
        notification.text = message;
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            notification.color = new Color(
                notification.color.r,
                notification.color.g,
                notification.color.b,
                Mathf.Lerp(1f, 0f, t / fadeTime));
            yield return null;

        }
    } 
 }
