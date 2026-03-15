namespace UniversitySystem.Models;

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public virtual string GetInfo()
    {
        return $"Bruker {Name} - {Email}";
    }
}