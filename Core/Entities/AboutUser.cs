using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public enum Roles
    {
        Administrator,
        Operator,
        Visitor
    }

    public class UserEntity
    {
        [Key]
        public int ID { get; set; }

        private string _Username = string.Empty;

        public string Username
        {
            get
            {
                return _Username;
            }

            set
            {
                _Username = value;
            }
        }

        private string _Password = string.Empty;

        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
            }
        }

        private string _Role = Roles.Administrator.ToString();

        public string Role
        {
            get
            {
                return _Role;
            }

            set
            {
                _Role = value;
            }
        }

        [NotMapped]
        public bool IsEnable { get; set; } = false;
    }
}