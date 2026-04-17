# Sistema de Gestão de Vendas

Aplicação desktop para gerenciamento de pessoas, produtos e pedidos, com persistência em JSON e interface em tema escuro.

## Tecnologias

- C# / .NET Framework 4.6
- WPF (Windows Presentation Foundation)
- Padrão MVVM
- Newtonsoft.Json
- LINQ

### Pré-requisitos

- Windows 10 ou superior
- .NET Framework 4.6 instalado
- Visual Studio 2019 ou superior

### Instalação

1. Clone o repositório: git clone https://github.com/junioreS09/coodeshtask
2. Abra o arquivo "WpfApp.sln" no Visual Studio
3. Restaure os pacotes NuGet (clique com o botão direito na solução → *Restore NuGet Packages*)
4. Compile e execute ("F5")

### Uso

- **Pessoas** — cadastre clientes com nome, CPF e endereço
- **Produtos** — cadastre produtos com nome, código e valor
- **Pedidos** — crie pedidos vinculados a uma pessoa, adicione produtos, selecione forma de pagamento e parcelamento

Os dados são salvos automaticamente em arquivos JSON na pasta "Data/" gerada na raiz do executável.
