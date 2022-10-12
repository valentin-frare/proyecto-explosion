using UnityEngine;

public class DestroyFallingProp : BaseDestructibleProp 
{
#if UNITY_EDITOR
    [ContextMenu("Destroy")]
    private void DestroyOnEditor() => Destroy();
#endif

    [SerializeField] private LeanTweenType easeType;

    private bool isDestroying;

    public override void Destroy()
    {
        Debug.Log("awa");
        if (isDestroying) return;

        isDestroying = true;

        base.Destroy();

        LeanTween.rotate(base.gameObject, new Vector3(-90f, 0f, Random.Range(-45, 45)), .1f).setEase(easeType).setOnComplete(
            () => { Destroy(base.gameObject, 1f); }
        );
    }
}