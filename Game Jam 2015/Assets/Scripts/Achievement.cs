using UnityEngine;

public class Achievement
{
    bool obtained, triggered;
    public static Texture2D unknownTexture = (Texture2D)Resources.Load("unknown");
    public Texture2D obtainedTexture;
    public string name, description;

    public Achievement(string nm, string desc, string tex)
    {
        name = nm;
        description = desc;
        obtainedTexture = (Texture2D)Resources.Load(tex);
    }

    public bool Obtained
    {
        get
        {
            if (PlayerPrefs.HasKey("achievement_" + name))
            {
                return (PlayerPrefs.GetInt("achievement_" + name) == 1);
            }
            else
            {
                Obtained = false;
                return false;
            }
        }

        set
        {
            PlayerPrefs.SetInt("achievement_" + name, value ? 1 : 0);
            obtained = value;
        }
    }

    public void Trigger()
    {
        if (!Obtained)
        {
            Obtained = true;
        }
    }

    void OnGUI()
    {
        if (triggered)
        {
            GUI.DrawTexture(new Rect(0,0,50,50),obtainedTexture);
            GUI.Label(new Rect(60, 10, 100, 40), "Achievement GET!");
        }
    }
}