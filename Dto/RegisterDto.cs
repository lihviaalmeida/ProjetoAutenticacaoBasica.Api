namespace ProjetoAutenticacaoBasica.Api.Dto;

public class RegisterDto
{
    public string Nome { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
