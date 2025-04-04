using GLTFast;
using UnityEngine;

namespace Models
{
    public class ItemMetadata
    {
        public string imageUrl;
        public string modelUrl;
        public string name;

        public Texture2D image;
        public GltfImport model;

        public void SetTexture (Texture2D texture)
        {
            image = texture;
        }
        public void SetModel (GltfImport modelImport)
        {
            model = modelImport;
        }
    }
}