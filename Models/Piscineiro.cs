﻿namespace PoolPiscinas.Models
{
    public class Piscineiro
    {
        public int PiscineiroID { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public int FranquiaID { get; set; }
        public Franquia Franquia { get; set; }
    }
}
