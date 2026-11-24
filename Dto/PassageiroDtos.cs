namespace ProjetoAutenticacaoBasica.Api.Dto
{
    public class PassageiroDtos
    {
    }

    public class  CriarPassageiroDtos
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

    }

    public class PassageiroInfoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}

