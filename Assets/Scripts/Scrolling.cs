using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private List<SpriteRenderer> sprites;
    private List<Transform> slides;
    [SerializeField] private float speed;
    private float cameraRange;
    private float slideWidth;
    private int firstSlideIndex = 0;
    private int lastSlideIndex;
    void Start()
    {
        slides = sprites.Select(s => s.gameObject.transform).ToList();
        slideWidth = sprites[0].bounds.size.x;
        cameraRange = camera.orthographicSize;
        slides = slides.OrderBy(s => s.position.x).ToList();
        lastSlideIndex = slides.Count - 1;
    }

    void Update()
    {
        ScrollSlides();
        MoveSlidesBackIfNeeded();
    }

    private void ScrollSlides()
    {
        slides.ForEach(s =>
        {
            float distance = Time.deltaTime * speed;
            s.transform.position = new Vector3(s.transform.position.x - distance, s.transform.position.y, s.transform.position.z);
        });
    }

    private void MoveSlidesBackIfNeeded()
    {
        var slide = slides[firstSlideIndex];
        float xDistanceToCamera = Mathf.Abs(slide.position.x - camera.transform.position.x);
        float maxDistance = Mathf.Max(cameraRange, slideWidth);
        if (xDistanceToCamera > maxDistance &&
        slide.position.x < camera.transform.position.x)
        {
            var lastSlide = slides[lastSlideIndex];
            slide.position = new Vector3(lastSlide.position.x + slideWidth, lastSlide.position.y, lastSlide.position.z);
            lastSlideIndex = firstSlideIndex;
            firstSlideIndex++;
            if (firstSlideIndex >= slides.Count)
            {
                firstSlideIndex = 0;
            }
        }
    }
}
