using SQLite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiApp3.MVVM.Model
{
    
    public class Users
    {
        [PrimaryKey, AutoIncrement, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Unique, SQLite.MaxLength(30)]
        public string UserName { get; set; }
        public string Password { get; set; }

        public Users Clone() => MemberwiseClone() as Users;
    }
}
