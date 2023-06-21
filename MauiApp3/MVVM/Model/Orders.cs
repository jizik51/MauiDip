using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MauiApp3.MVVM.Model
{
    public class Orders
    {
        [PrimaryKey, AutoIncrement, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Unique, SQLite.MaxLength(30)]
        public string Service { get; set; }
        public string UserData{ get; set; }
        public string Date { get; set; }
        public string PayMethod { get; set; }
        public string OrderStatus { get; set; }

    }
}
