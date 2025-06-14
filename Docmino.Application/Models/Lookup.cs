namespace Docmino.Application.Models;
public class Lookup<TKey>
{
    public TKey Id { get; set; }
    public string Name { get; set; }
}

public class Lookup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

