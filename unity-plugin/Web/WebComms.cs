using System.Runtime.InteropServices;

public class WebComms
{
    [DllImport("__Internal")]
    public static extern void Connect();
}
