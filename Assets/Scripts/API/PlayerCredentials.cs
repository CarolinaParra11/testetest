using System;

[Serializable]
public class PlayerCredentials
{
    public string login;
    public string senha;
    public string idDispositivo;
    public string versao;

    public PlayerCredentials(string log, string sen, string id, string versao)
    {
        this.login = log;
        this.senha = sen;
        this.idDispositivo = id;
        this.versao = versao;
    }
}


[Serializable]
public class AulaInfo
{
    public bool v1;
    public int numeroAula;
    public bool introducao;

    public AulaInfo(bool v, int num, bool intro)
    {
        this.v1 = v;
        this.numeroAula = num;
        this.introducao = intro;
    }
}
