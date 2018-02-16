namespace API.Models
{

    /// <summary>
    /// Provides a reusable command that helps maintain SOLID coding practices
    /// <para>Source: <see href="https://alexandrebrisebois.wordpress.com/2012/06/21/build-reusable-testable-commands-part-1/"/></para>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    interface IModelCommand<in TModel>
    {
        void Apply(TModel model);
    }
}
