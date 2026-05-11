using UnityEngine;
using UnityEngine.UIElements;

public enum OperatingSystemName
{
    Ubuntu,
    Fedora,
    Debian,
    KaliLinux,
    LinuxMint,
}

[System.Serializable]
public struct OperatingSystem
{
    public VisualTreeAsset DesktopLayout;
    public OperatingSystemName Name;
    public string DisplayName;
    public bool isPay;
    public string Description;
    public Texture2D icon;
    public Color PrimaryColor;
    public Color SecondaryColor;
    public Color ThirdColor;
    public Color TextColor;

    public override string ToString()
    {
        return "Name: " + Name.ToString() + " | Display Name: " + DisplayName + "\nPrimary color: " + PrimaryColor + " | Second color: " + SecondaryColor + " | Third Color: " + ThirdColor;
    }
}