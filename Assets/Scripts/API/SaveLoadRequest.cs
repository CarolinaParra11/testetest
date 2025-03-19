using System.Collections.Generic;

public class SaveLoadRequest
{
    public string versao { get; set; }
    public bool v2 { get; set; }
    public int type { get; set; }
    public string nome { get; set; }
    public int coins { get; set; }
    public int level { get; set; }
    public bool blue { get; set; }
    public bool bonus1 { get; set; }
    public bool bonus2 { get; set; }
    public int professionID { get; set; }
    public bool english { get; set; }
    public bool ensurance { get; set; }
    public bool dentist { get; set; }
    public int vault { get; set; }
    public int gift1 { get; set; }
    public int gift2 { get; set; }
    public int gift3 { get; set; }
    public int safe1 { get; set; }
    public int safe2 { get; set; }
    public int safe3 { get; set; }
    public int v2l20choice { get; set; }
    public int v2l26choice { get; set; }
    public List<int> v1l16choices { get; set; }
    public bool ended { get; set; }

    public bool bloqueado { get; set; }
}
