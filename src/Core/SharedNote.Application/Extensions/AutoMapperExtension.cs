using AutoMapper;

namespace SharedNote.Application.Extensions
{
    public static class AutoMapperExtension
    {
        public static TResult MergeInto<TResult>(this IMapper mapper, object item1, object item2)
        {
            return mapper.Map(item2, mapper.Map<TResult>(item1));
        }
    }
}
