using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class ToastBase : MonoBehaviour
{
    public static ToastBase instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        VisualElement panel = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("ToastPanel");
        DOVirtual.Vector3(
            new Vector3(0, 1, 1),
            Vector3.one,
            1.5f,
            (value) => panel.style.scale = value
        ).SetEase(Ease.OutBounce);
    }

    // Update is called once per frame
    public void DestroyToast()
    {
        VisualElement panel = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("ToastPanel");
        DOVirtual.Vector3(
            panel.style.scale.value.value,
            new Vector3(0, 1, 1),
            1.5f,
            (value) => panel.style.scale = value
        ).SetEase(Ease.OutBounce);
        Destroy(gameObject, 1.5f);
    }
    public static void CreateToast(Object original)
    {
        if (instance == null)
        {
            GameObject obj = Instantiate(original).GameObject();
            instance = obj.GetComponent<QuitConfirmToast>();
        
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
