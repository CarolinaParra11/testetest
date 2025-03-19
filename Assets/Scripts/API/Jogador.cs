using System;

[Serializable]
public class Jogador
{
    // Jogador
    public string id;
    public string nome;
    public string v2;
    public string idade;
    public Boolean b2b;
}

[Serializable]
public class JogadorData
{
    public Jogador jogador;
    public string token;
}

[Serializable]
public class AulaData
{
    public string url;
}

[Serializable]
public class Root<T>
{
    public T data;
    public string success;
    public string message;
}