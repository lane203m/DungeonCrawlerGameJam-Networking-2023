using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashDeath : MonoBehaviour
{
    public Image lifeImage;
    public Image deathImage;

    public Image blackImage;

    public bool setFadeToBlack = true;

    public Image blackClearImage;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(deathImage && lifeImage && blackImage && blackClearImage){
            if(deathImage.GetComponent<Image>().color.a > 0){
                var imageColor = deathImage.GetComponent<Image>().color;
                imageColor.a -= 0.01f;
                
                deathImage.GetComponent<Image>().color = imageColor;
            }
            if(lifeImage.GetComponent<Image>().color.a > 0){
                var imageColor = lifeImage.GetComponent<Image>().color;
                imageColor.a -= 0.01f;
                
                lifeImage.GetComponent<Image>().color = imageColor;
            }
        }
        if(blackImage){
            if(blackImage.GetComponent<Image>().color.a > 0f){
                var imageColor2 = blackImage.GetComponent<Image>().color;
                imageColor2.a = imageColor2.a + 0.01f;
                blackImage.GetComponent<Image>().color = imageColor2;
            }
        }
        if(blackClearImage){
            if(blackClearImage.GetComponent<Image>().color.a > 0){
                var imageColor = blackClearImage.GetComponent<Image>().color;
                imageColor.a -= 0.01f;
                blackClearImage.GetComponent<Image>().color = imageColor;
            }
        }
    }

    public void StartFlashDeath()
    {
        var imageColor = deathImage.GetComponent<Image>().color;
        imageColor.a = 0.8f;

        deathImage.GetComponent<Image>().color = imageColor;
    }

    public void StartFlashLife()
    {
        var imageColor = lifeImage.GetComponent<Image>().color;
        imageColor.a = 0.8f;

        lifeImage.GetComponent<Image>().color = imageColor;    
    }

    public void FadeToBlack()
    {
        if(setFadeToBlack){
            setFadeToBlack = false;
            var imageColor = blackImage.GetComponent<Image>().color;
            imageColor.a = 0.01f;


            blackImage.GetComponent<Image>().color = imageColor;     
        }
    }

    public void FadeIn()
    {
        var imageColor = blackImage.GetComponent<Image>().color;
        imageColor.a = 1f;

        blackClearImage.GetComponent<Image>().color = imageColor;    
    }
}
