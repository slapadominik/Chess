using System.Collections.Generic;
using Chess.API.Entity.Interfaces;

namespace Chess.API.Helpers
{
    public static class TableNumberCalculator
    {
        public static int CalculateFreeTableNumber(IList<ITable> tables)
        {
            var tableNumber = -1;
            for (int i = 1; i < tables.Count; i++)
            {
                if (tables[i].Number != i)
                {
                    tableNumber = i;
                }
            }

            if (tableNumber == -1)
            {
                tableNumber = tables.Count + 1;
            }

            return tableNumber;
        }
    }
}