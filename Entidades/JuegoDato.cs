namespace JuegosApi.Entidades
{
    public class JuegoDato
    {
        public int JuegoId { get; set; }
        public int DatoId { get; set; }
        public int Orden { get; set; }
        public Juego Juego { get; set; }
        public Dato Dato { get; set; }
    }
}
