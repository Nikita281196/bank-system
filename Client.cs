using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    struct Client
    {
        #region Автосвойства
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public long PhoneNumber { get; set; }
        public string PassData { get; set; }
        public string DateTime { get; set; }
        public string DataChanged { get; set; }
        public string TypeOfChanged { get; set; }
        public string WhoChanged { get; set; }
        #endregion
        #region Конструкторы
        public Client(string Surname, 
            string Name, 
            string Patronymic,
            long PhoneNumber,
            string PassData, 
            string DateTime, 
            string DataChanged, 
            string TypeOfChanged, 
            string WhoChanged)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Patronymic = Patronymic;
            this.PhoneNumber = PhoneNumber;
            this.PassData = PassData;
            this.DateTime = DateTime;
            this.DataChanged = DataChanged;
            this.TypeOfChanged = TypeOfChanged;
            this.WhoChanged = WhoChanged;
        }
        public Client(string Surname, string Name, string Patronymic, long PhoneNumber, string PassData) :
            this(Surname,Name,Patronymic,PhoneNumber,PassData,String.Empty,String.Empty,String.Empty,String.Empty)
        {

        }
        #endregion
    }
}
