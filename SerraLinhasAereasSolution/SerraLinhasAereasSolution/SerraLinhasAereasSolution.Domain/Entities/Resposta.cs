
namespace SerraLinhasAereasSolution.Domain.Entities
{
    public class Resposta
    {
        public int Status { get; set; }
        public string Mensagem { get; set; }

        public Resposta(int status, string mensagem)
        {
            this.Status = status;
            this.Mensagem = mensagem;
        }
        public Resposta()
        {

        }
    }
}
