using System;

public class RelatorioRequest
{
    public Categoria[] categorias { get; set; }
}

public class Categoria
{
    public string id;
    public float value;
}