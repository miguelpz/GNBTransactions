using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNB.Transactions.Domain.Rates
{
    public class Currency
    {
        public string id { get; private set; }

        public Dictionary<Currency, Decimal> conversiones { get; private set; }

        public Currency (string id)
        {
            this.id = id;
            conversiones = new Dictionary<Currency, decimal>();
        }


        public void procesarRates(List<Currency> pila)
        {
            pila.Add(this);
            foreach (var conversion in conversiones.ToList())
            {
                if (!pila.Contains(conversion.Key))
                {
                    conversion.Key.procesarRates(pila);
                    foreach(var conversion2 in conversion.Key.conversiones.ToList())
                    {
                        if (conversion2.Key != this && !conversiones.ContainsKey(conversion2.Key))
                        {
                            conversiones.Add(conversion2.Key, conversion2.Value * conversion.Value);
                        }
                    }
                }
            }
            pila.Remove(this);
        }
    }
}




