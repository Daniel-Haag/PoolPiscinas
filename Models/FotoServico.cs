namespace PoolPiscinas.Models
{
    public class FotoServico
    {
        public int FotoServicoID { get; set; }
        public int OrdemServicoID { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public string Base64 { get; set; }
    }
}
