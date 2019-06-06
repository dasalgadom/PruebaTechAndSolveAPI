using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Result.Base;

namespace PruebaTechAndSolve.Business.Interface
{
    public interface IOperationsBusiness
    {
        /// <summary>
        /// Este metodo recibe la lista cargada desde el archivo y procesa la cantidad de viajes, posteriormente, guarda la trazabilidad del proceso.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        BusinessResult<List<string>> CalculateProcess(string[] elements);

        /// <summary>
        /// En este metodo se guarda la trazabilidad del proceso.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="urlProcess"></param>
        /// <returns></returns>
        Task<BusinessResult<bool>> SaveProcess(int document, string urlProcess);

    }
}
