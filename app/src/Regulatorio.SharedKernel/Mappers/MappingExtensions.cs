using Regulatorio.SharedKernel.Entities;
using Regulatorio.SharedKernel.Requests;
using Regulatorio.SharedKernel.Responses;

namespace Regulatorio.SharedKernel.Mappers
{
    public static class MappingExtensions
    {
        public static TResponse ToResponse<TResponse>(this BaseEntity entity) where TResponse : IResponse
        {
            entity.Guard("The parameter can't be null for this operation", nameof(entity));

            return entity.Map<TResponse>();
        }

        public static TEntity ToEntity<TEntity>(this IResponse response) where TEntity : BaseEntity
        {
            response.Guard("The parameter can't be null for this operation", nameof(response));

            return response.Map<TEntity>();
        }

        public static TEntity ToEntity<TEntity>(this BaseEntityRequest request) where TEntity : BaseEntity
        {
            request.Guard("The parameter can't be null for this operation", nameof(request));

            return request.Map<TEntity>();
        }

        private static TDestination Map<TDestination>(this object source)
        {
            return AutoMapperConfiguration.Mapper.Map<TDestination>(source);
        }
    }
}