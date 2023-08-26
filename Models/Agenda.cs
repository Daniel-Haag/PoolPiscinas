using System;

namespace PoolPiscinas.Models
{
    public class Agenda
    {
        public int AgendaID { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
