using UnityEngine;

class pickupInfo
{
    bool obtained;
    public static Texture2D unknownTexture = (Texture2D)Resources.Load("unknownPickup.png");
    public Texture2D ingameTexture, mathematicalTexture;
    public string name, description;

    public pickupInfo(string nm, string descr, Texture2D ingTex, Texture2D mathTex)
    {
        Obtained = false;
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