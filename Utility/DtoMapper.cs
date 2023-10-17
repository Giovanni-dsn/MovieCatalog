#pragma warning disable CS8600

using System.Reflection;

namespace MovieCatalog.Utils;

public static class DtoMapper
{
    public static void MapDtoToModel<TDto, TModel>(TDto dto, TModel model)
    {
        PropertyInfo[] dtoProperties = typeof(TDto).GetProperties();
        PropertyInfo[] modelProperties = typeof(TModel).GetProperties();

        foreach (var dtoProperty in dtoProperties)
        {
            var correspondingModelProperty = Array.Find(modelProperties, p => p.Name == dtoProperty.Name);

            if (correspondingModelProperty != null)
            {
                object dtoValue = dtoProperty.GetValue(dto);
                correspondingModelProperty.SetValue(model, dtoValue);
            }
        }
    }
}