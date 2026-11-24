namespace ProjetoAutenticacaoBasica.Api.Dto
{
    public class Passagem
    {
    }

    public class EmitirPassagemDtos
    {
        public int VooId { get; set; }
        public int PassageiroId { get; set; }
        public int Assento { get; set; }
    }

    public class PassagemdetalhesDto
    {
        public int Id { get; set; }
        public int CodigoVoo { get; set; }
        public string Trajeto { get; set; }
        public int Nomepassageiro { get; set; }
        public int Assento { get; set; }
    }
}
