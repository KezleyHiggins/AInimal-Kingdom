using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class AnimalAnimationPlayer : MonoBehaviour
{

    #region Variables

    [SerializeField] SpriteRenderer animalExpression, animalSoundSprite;
    [SerializeField] Sprite happyExpression, sadExpression;

    #endregion

    #region Showing Expressions

    public void ShowHappyExpression()
    {
        if (gameObject.activeSelf == false) return;
        animalExpression.sprite = happyExpression;
        animalExpression.gameObject.SetActive(true);
        StartCoroutine("TurnOffExpressionAfterDelay");
    }

    public void ShowSadExpression()
    {
        if (gameObject.activeSelf == false) return;
        animalExpression.sprite = sadExpression;
        animalExpression.gameObject.SetActive(true);
        StartCoroutine("TurnOffExpressionAfterDelay");
    }

    IEnumerator TurnOffExpressionAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        animalExpression.gameObject.SetActive(false);
    }

    #endregion

    #region Display Music Note

    public void DisplayMusicNote()
    {
        if (gameObject.activeSelf == false) { return; }
        animalSoundSprite.gameObject.SetActive(true);
        StartCoroutine("TurnOffMusicSpriteAfterDelay");
    }

    IEnumerator TurnOffMusicSpriteAfterDelay()
    {
        yield return new WaitForSeconds(0.25f);
        HideMusicNote();
    }

    public void HideMusicNote()
    {
        animalSoundSprite.gameObject.SetActive(false);
    }

    #endregion

}
