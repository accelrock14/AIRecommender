using AIRecommender.DataLoader;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AIRecommender.DataAggrigator
{
    public interface IRatingsAggrigator
    {
        Dictionary<string, List<int>> Aggrigate(BookDetails bookDetails, Preference preferance);
    }
}
