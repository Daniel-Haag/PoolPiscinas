namespace PoolPiscinas.Models
{
    public class Conta
    {
        public int ContaID { get; set; }
        public bool Pagar { get; set; }
        public decimal Valor { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
