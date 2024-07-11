using MTechTest.Context;
using MTechTest.Models;
using System.Globalization;
using System.Text;

namespace MTechTest.Services
{
    public class RfcValidator
    {
        public bool ValidateRfc(Employee employee)
        {
            StringBuilder rfc = new StringBuilder();

            int index = employee.LastName.IndexOf(" ");
            string firstLastName = employee.LastName.Substring(0, index);
            string secondLastName = employee.LastName.Substring(index + 1);

            rfc.Append(firstLastName.Substring(0, 1));
            rfc.Append(FirstVowel(firstLastName));

            rfc.Append(secondLastName.Substring(0, 1));
            rfc.Append(employee.Name.Substring(0, 1));

            rfc.Append(employee.BornDate.ToString("yyMMdd", CultureInfo.InvariantCulture));

            return String.Compare(employee.RFC, rfc.ToString(), true) == 0 ? true : false;
        }

        public bool CheckUniqueRfc(string rfc, EmployeeContext context)
        {
            return !context.Employees.Any(e => e.RFC == rfc);
        }

        public char FirstVowel(string lastname)
        {
            string vowels = "AEIOU";

            return lastname
                .Substring(1)
                .ToUpper()
                .FirstOrDefault(c => vowels.Contains(c), 'X');
        }
    }
}
