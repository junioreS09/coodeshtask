using System.Linq;

namespace WpfApp.Helpers
{
     public static class ValidadorCpf
     {
        public static bool Validar(string cpf)
        {
        cpf = cpf.Replace(".", "").Replace("-", "").Trim();

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        var multiplicadores1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicadores2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var soma = tempCpf.Select((t, i) => (t - '0') * multiplicadores1[i]).Sum();
        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = tempCpf.Select((t, i) => (t - '0') * multiplicadores2[i]).Sum();
        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith($"{digito1}{digito2}");
        }
     }   
}