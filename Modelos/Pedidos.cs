class Pedidos{
    public int nOrdem;
    public int nif;
    public string codigo;
    public int tempo;
    public int distancia;
    public Pedidos(int nOrdem, int nif, string codigo, int tempo, int distancia){
        this.nOrdem = nOrdem;
        this.nif = nif;
        this.codigo = codigo;
        this.tempo = tempo;
        this.distancia = distancia;
    }
}