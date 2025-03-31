using SQLite;

namespace MauiAppMinhasComprasFehFinal.Models
{
    public class Produto
    {
        string _descricao;
        

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao {
            get => _descricao;
            set
        {
                if(value == string.Empty) 
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value;
            }
        
        }

        string _tipo;
        public string Tipo
        {
            get => _tipo;
            set
            {
                if (value == string.Empty)
                {
                    throw new Exception("Por favor, preencha o tipo");
                }
                _tipo = value;
            }

        }
        public double Quantidade {  get; set; }
        public double Preco {  get; set; }

        public double Total { get => Quantidade * Preco; }
    }
}
