using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePleno.Controllers;
using TestePleno.Models;

namespace TestePleno
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inserção das Operadoras

            //Foi adicionado o arquivo OperatorController.cs para manter a estrutura do projeto
            var operatorController = new OperatorController();

            operatorController.InsertOperator(new Operator { Code = "OP01", Id = Guid.NewGuid() });
            operatorController.InsertOperator(new Operator { Code = "OP02", Id = Guid.NewGuid() });

            //looping para mais de uma inserção de Fare
            while (true)
            {
                var fare = new Fare();
                fare.Id = Guid.NewGuid();

                Console.WriteLine("Informe o valor da tarifa a ser cadastrada:");
                var fareValueInput = Console.ReadLine();
                fare.Value = decimal.Parse(fareValueInput);

                Console.WriteLine("Informe o código da operadora para a tarifa:");
                Console.WriteLine("Exemplos: OP01, OP02, OP03...");
                var operatorCodeInput = Console.ReadLine();

                var fareController = new FareController();
                fareController.CreateFare(fare, operatorCodeInput);

                Console.WriteLine("Tarifa cadastrada com sucesso!");
            }
        }
    }
}
