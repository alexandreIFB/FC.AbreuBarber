﻿

namespace FC.AbreuBarber.Domain.SeedWork.SearchableRepository
{
    public interface ISearchableRepository<TAggregate> where TAggregate : AggregateRoot
    {
        Task<SearchOutput<TAggregate>> Search(SearchInput input, CancellationToken cancellationToken);
    }
}
