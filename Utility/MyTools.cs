using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class MyTools
    {
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> source, int page, byte pageSize)
        {
            int totalSkipped = page * pageSize - pageSize;
            int to = page * pageSize;
            var pagedElements = source.TakeRange(totalSkipped, to);

            return pagedElements;
        }

        private static IEnumerable<T> TakeRange<T>(this IEnumerable<T> source, int from, int to)
        {
            List<T> list = new();

            for (int i = from; i < to; i++)
            {
                T element = source.ElementAtOrDefault(i);
                if (element is null)
                {
                    break;
                }
                list.Add(element);
            }
            return list;
        }
    }
}
