namespace PoolPiscinas.Models
{
    public class OrdemServico
    {
        public int OrdemServicoID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int AgendaID { get; set; }
        public Agenda Agenda { get; set; }
    }
}
