using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartVisual : MonoBehaviour
{
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;

    private List<HeartImage> _heartImageList;

    private void Awake()
    {
        _heartImageList = new List<HeartImage>();    
    }

    private void Start()
    {
        CreateHeartImage(new Vector2 (0, 0)).SetHeartFragments(1);
        CreateHeartImage(new Vector2 (30, 0)).SetHeartFragments(0);
        CreateHeartImage(new Vector2 (60, 0)).SetHeartFragments(1);
    }
    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        //Create Game Object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));
        //Set as child of this transform
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;
        
        //Locate and Size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
 
        //Set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = fullHeartSprite;

        HeartImage heartImage = new HeartImage(heartImageUI);
        _heartImageList.Add(heartImage);

        return heartImage;

    }

    //Represent a single Heart
    public class HeartImage 
    {
        private Image _heartImage;
        private HealthHeartVisual _healthHeartVisual;
        private Image heartImageUI;

        public HeartImage(Image heartImageUI)
        {
            this.heartImageUI = heartImageUI;
        }

        public HeartImage(HealthHeartVisual healthHeartVisual, Image heartImage)
        {
            this._healthHeartVisual = healthHeartVisual;
            this._heartImage = heartImage;
        }

        public void SetHeartFragments (int fragments) 
        {
            switch (fragments) 
            {
                case 0:
                    _heartImage.sprite = _healthHeartVisual.fullHeartSprite;
                    break;
                case 1:
                    _heartImage.sprite = _healthHeartVisual.halfHeartSprite;
                    break;
            }    
        }

    }

}