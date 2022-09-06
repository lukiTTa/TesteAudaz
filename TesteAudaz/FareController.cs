using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestePleno.Models;
using TestePleno.Services;

namespace TestePleno.Controllers
{
    public class FareController
    {
        private OperatorService _operatorService;
        private FareService _fareService;
        private Repository _repository;

        public FareController()
        {
            _operatorService = new OperatorService();
            _fareService = new FareService();
            _repository = new Repository();
        }

        public void CreateFare(Fare fare, string operatorCode)
        {
            var selectedOperator = _operatorService.GetOperatorByCode(operatorCode);
            fare.OperatorId = selectedOperator.Id;

            //Atualização do status e da data de criação da fare
            fare.Status = 1;
            fare.createdAt = DateTime.Now;

            //Comparativo para verificação da regra de negócio especificada
            var fares = _repository.GetAll<Fare>();
            if (fares.Find(x => x.Status == 1 && 
                                x.Value == fare.Value && 
                                x.OperatorId == fare.OperatorId && 
                                ((fare.createdAt - x.createdAt).TotalDays <= 180)) == null)
                _fareService.Create(fare);
            else
                throw new Exception("Uma 'Fare' só pode ser cadastrada caso a operadora não possua nenhuma tarifa ativa de mesmo valor dentro de um período de 6 meses.");
        }
    }
}
