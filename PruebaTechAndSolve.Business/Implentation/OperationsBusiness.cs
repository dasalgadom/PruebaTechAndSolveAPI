using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Result.Base;
using PruebaTechAndSolve.Business.Interface;
using System.Configuration;
using PruebaTechAndSolve.Data.Interface;
using PruebaTechAndSolve.Data.Implementation;
using PruebaTechAndSolve.Dto;

namespace PruebaTechAndSolve.Business.Implentation
{
    public class OperationsBusiness : IOperationsBusiness
    {

        // <summary>
        /// Instancia de la clase BuyerData
        /// </summary>
        IUserTripsData _iusertripsdata;

        /// <summary>
        /// Constructor de la clase
        /// </summary>    
        public OperationsBusiness()
        {
            _iusertripsdata = new UserTripsData();
        }



        /// <summary>
        /// Constructor de la clase
        /// </summary>    
        public OperationsBusiness(IUserTripsData iusertripsdata)
        {
            _iusertripsdata = iusertripsdata;

        }

        public BusinessResult<List<string>> CalculateProcess(string[] elements)
        {
            try
            {
                var resultTrip = CalculateTrip(elements);

                return BusinessResult<List<string>>.Success(resultTrip.Result, "Operación Correcta");
            }
            catch (Exception ex)
            {
                return BusinessResult<List<string>>.Issue(null, "Operación Incorrecta", ex);

            }
        }

        public async Task<BusinessResult<UserTripsDto>> SaveProcess(int document, string urlProcess)
        {
            try
            {
                var userTripsDto = new UserTripsDto()
                {
                    Document = document,
                    DateProcess = System.DateTime.Now,
                    UrlFileProcess = urlProcess
                };
                var result = await _iusertripsdata.CreateAsync(userTripsDto);

                return BusinessResult<UserTripsDto>.Success(result, "Operación Correcta");
            }
            catch (Exception ex)
            {
                return BusinessResult<UserTripsDto>.Issue(null, "Operación Incorrecta", ex);
            }
        }


        /// <summary>
        /// En este caso este metodo calcula la cantidad de viajes que realiza por día.
        /// </summary>
        /// <param name="elementsDay"></param>
        /// <returns></returns>
        private BusinessResult<List<string>> CalculateTrip(string[] elementsDay)
        {
            try
            {
                var resultElements = GetElementsDay(elementsDay);
                var trip = new List<string>();
                int counTrip = 0;
                int day = 0;
                var WeightMin = int.Parse(ConfigurationManager.AppSettings["WeightMin"]);
                foreach (var item in resultElements.Result)
                {
                    day++;
                    var list = item.OrderByDescending(x => x).ToList();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i] <= WeightMin)
                        {
                            int cont = 2;
                            var operador = 0;
                            if (i == list.Count - 1 && list[1] < WeightMin)
                            {
                                break;
                            }
                            while (operador <= WeightMin)
                            {
                                operador = list[i] * cont;
                                cont++;
                            }
                            counTrip++;
                            int delete = cont - 2;
                            list = list.Take(list.Count() - delete).ToList();
                        }
                        else
                        {
                            counTrip++;
                        }
                    }
                    var result = String.Format("{0} {1}: {2}", "Case #", day, counTrip);
                    trip.Add(result);
                    counTrip = 0;
                }
                return BusinessResult<List<string>>.Success(trip, "Operación Correcta");
            }
            catch (Exception ex)
            {
                return BusinessResult<List<string>>.Issue(null, "Operación Incorrecta", ex);
            }
        }

        /// <summary>
        /// Este metodo crea una lista separando los elementos a procesar por dia.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        private BusinessResult<List<List<int>>> GetElementsDay(string[] elements)
        {
            try
            {
                int workDay = Convert.ToInt32(elements[0]);
                if (workDay > int.Parse(ConfigurationManager.AppSettings["WorkDay"]))
                {
                    throw new Exception("El número de días supera el máximo permitido");
                }
                int elementos = Convert.ToInt32(elements[1]);
                if (workDay > int.Parse(ConfigurationManager.AppSettings["Elements"]))
                {
                    throw new Exception("El número elementos supera el máximo permitido");
                }
                var element = new List<int>();
                var days = new List<List<int>>();
                var limit = elementos + 1;

                for (int i = 2; i < elements.Length; i++)
                {
                    if (elements[i] != "")
                    {
                        if (i > limit)
                        {
                            days.Add(element);
                            if (i < elements.Length)
                            {
                                limit = limit + Convert.ToInt32(elements[i]) + 1;
                            }
                            element = new List<int>();
                        }
                        else
                        {
                            element.Add(Convert.ToInt32(elements[i]));
                        } 
                    }
                }
                days.Add(element);

                return BusinessResult<List<List<int>>>.Success(days, "Operación Correcta");
            }
            catch (Exception ex)
            {
                return BusinessResult<List<List<int>>>.Issue(null, "Operación Incorrecta", ex);
            }
        }
    }
}
