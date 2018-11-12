using System.Threading.Tasks;

namespace Chess.API.Hubs.Interfaces
{
    public interface IValuesClient
    {
        /// <summary>
        /// This event occurs when a value is posted to the
        /// ValuesController.
        /// </summary>
        /// <param name="value">The value that has been created.</param>
        Task PostValue(string value);

        /// <summary>
        /// This event occurs when a value is deleted using the
        /// ValuesController.
        /// </summary>
        /// <param name="value">The value that has been deleted</param>
        Task DeleteValue(string value);
    }
}