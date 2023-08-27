﻿namespace PoolPiscinas.Models
{
    public class Documentos
    {
        public int DocumentoID { get; set; }
        public int OrdemServicoID { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public int ContaID { get; set; }
        public Conta Conta { get; set; }
        public string Base64Arquivo { get; set; }
    }
}
