namespace Hosta_Hotel.Entities;

public abstract record Persoana
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; init; }
    public string UsernameID { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool LoggedIn { get; set; } = false;
}