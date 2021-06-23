using System.Collections.Generic;

namespace PokeApi.AppService.Framework
{
    public class ServiceResult<TResult>
    {
        public TResult Data { get; set; }
        public bool ExecutedSuccesfully { get; set; } = true;
        public List<string> Messages { get; } = new List<string>();

        public void AddErrorMessage(string message)
        {
            ExecutedSuccesfully = false;

            AddMessage(message);
        }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}
