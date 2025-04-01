using UnityEngine;

namespace Models
{
    public class ItemMetadata
    {
        public string imageUrl;
        public string modelUrl;
        public string name;

        public Texture2D image;
        public GameObject model;

        public void SetTexture (Texture2D texture)
        {
            image = texture;
        }
        public void SetModel (GameObject modelPrefab)
        {
            model = modelPrefab;
        }
    }
}