
namespace SerraLinhasAereasSolution.Domain.Entities
{
    public class Cliente
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public Cliente()
        {

        }

        public Cliente(string cpf, string nome, string sobrenome, string cep, string rua, int numero, string bairro, string cidade, string estado, string complemento)
        {
            Cpf = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            NomeCompleto = $"{nome} {sobrenome}";
            Cep = cep;
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Complemento = complemento;
        }

        public static bool CpfValido(string cpf)
        {
            return cpf.Length == 11;
        }
    }
}
