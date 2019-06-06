using Operations.Bussines.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Bussines.Implementation
{
    public class OperationBussines : IOperationBussines
    {
        public Task<List<string>> CalculateTrip(List<int> elements)
        {
            try
            {
                var ElementDay = await UploadFile();
                var trip = new List<string>();
                int counTrip = 0;
                int day = 0;
                foreach (var item in ElementDay)
                {
                    day++;
                    var list1 = item.OrderByDescending(x => x).ToList();
                    for (int i = 0; i < list1.Count; i++)
                    {
                        if (list1[i] <= 50)
                        {
                            int cont = 2;
                            var operador = 0;
                            if (i == list1.Count - 1 && list1[1] < 50)
                            {
                                break;
                            }
                            while (operador <= 50)
                            {
                                operador = list1[i] * cont;
                                cont++;
                            }
                            counTrip++;
                            int delete = cont - 2;
                            list1 = list1.Take(list1.Count() - delete).ToList();
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


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
