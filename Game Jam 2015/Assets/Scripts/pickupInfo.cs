using UnityEngine;

class pickupInfo
{
    bool obtained;
    public static Texture2D unknownTexture = (Texture2D)Resources.Load("unknownPickup.png");
    public Texture2D ingameTexture, mathematicalTexture;
    public string name, description;

    public pickupInfo(string nm, string desc, string ingTex, string mathTex)
    {
        Obtained = false;
        name = nm;
        description = desc;
        ingameTexture = (Texture2D)Resources.Load(ingTex);
        mathematicalTexture = (Texture2D)Resources.Load(mathTex);
    }

    public bool Obtained
    {
        get
        {
            return obtained;
        }

        set
        {
            obtained = value;
        }
    }
}