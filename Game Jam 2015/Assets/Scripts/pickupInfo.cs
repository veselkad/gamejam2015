using UnityEngine;

public class pickupInfo
{
    bool obtained;
    public static Texture2D unknownTexture = (Texture2D)Resources.Load("unknown");
    public Texture2D ingameTexture, mathematicalTexture;
    public string name, description;

    public pickupInfo(string nm, string desc, string ingTex, string mathTex)
    {
        name = nm;
        description = desc;
        ingameTexture = (Texture2D)Resources.Load(ingTex);
        //Debug.Log(ingTex);
        //Debug.Log(ingameTexture);
        mathematicalTexture = (Texture2D)Resources.Load(mathTex);
    }

    public bool Obtained
    {
        get
        {
            if (PlayerPrefs.HasKey("pickup_" + name))
            {
                return (PlayerPrefs.GetInt("pickup_" + name) == 1);
            }
            else
            {
                Obtained = false;
                return false;
            }
        }

        set
        {
            PlayerPrefs.SetInt("pickup_" + name, value ? 1 : 0);
            obtained = value;
        }
    }
}