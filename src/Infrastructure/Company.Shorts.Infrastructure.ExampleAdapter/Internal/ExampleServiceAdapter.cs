namespace Company.Shorts.Infrastructure.ExampleAdapter.Internal
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Blocks.Application.Contracts;
    using Company.Shorts.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class ExampleServiceAdapter : IExampleAdapter
    {
        public async Task CreateAsync(Example example, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task<List<Example>> GetAsync(CancellationToken cancellationToken)
        {
            List<Example> examples = new()
            {
                new(SystemGuid.NewGuid, string.Empty),
                new(SystemGuid.NewGuid, string.Empty),
            };

            return await Task.FromResult(examples);
        }

        public async Task<Example> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            Example example = new(SystemGuid.NewGuid, string.Empty);

            return await Task.FromResult(example);
        }

        public async Task UpdateAsync(Example example, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}
